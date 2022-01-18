namespace EventHorizon.Game.Editor.Zone.Editor.Services.Service;

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Connection.Shared;
using EventHorizon.Connection.Shared.Unauthorized;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Core.Services.Model;
using EventHorizon.Game.Editor.Zone.Editor.Services.Api;
using EventHorizon.Game.Editor.Zone.Editor.Services.Connection;

using MediatR;

using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

public class SignalrZoneEditorServices : ZoneEditorServices
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly ILogger _logger;
    private readonly IMediator _mediator;

    public ZoneEditorApi Api { get; private set; }

    private string _currentZoneId = string.Empty;
    private bool _initialized;
    private HubConnection? _connection;

    public SignalrZoneEditorServices(
        ILoggerFactory loggerFactory,
        ILogger<SignalrZoneEditorServices> logger,
        IMediator mediator
    )
    {
        _loggerFactory = loggerFactory;
        _logger = logger;
        _mediator = mediator;

        Api = new SignalrZoneEditorApi(
            _loggerFactory.CreateLogger<SignalrZoneEditorApi>(),
            null
        );
    }

    public async Task<StandardCommandResult> Connect(
        string accessToken,
        CoreZoneDetails zoneDetails,
        CancellationToken cancellationToken
    )
    {
        if (_initialized && zoneDetails.Id == _currentZoneId)
        {
            return new();
        }
        try
        {
            if (_connection != null)
            {
                await _connection.DisposeAsync();
                _connection = null;
            }
            _initialized = true;
            _currentZoneId = zoneDetails.Id;
            _connection = new HubConnectionBuilder()
                .WithAutomaticReconnect(
                    new TimeSpan[]
                    {
                        TimeSpan.FromSeconds(0),
                        TimeSpan.FromSeconds(2),
                        TimeSpan.FromSeconds(5),
                        TimeSpan.FromSeconds(30),
                        TimeSpan.FromSeconds(30),
                    }
                )
                .WithUrl(
                    $"{zoneDetails.ServerAddress}/systemEditor",
                    options =>
                    {
                        // options..LogLevel = SignalRLogLevel.Error;
                        options.AccessTokenProvider = () =>
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
                            accessToken.FromResult();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
                        // TODO: [BUG] - Look at this on a later date.
                        // Their is an issue with the sending of messages larger
                        //  than some arbitrary limit.
                        // This then causes the JSON to be broken up a weird spots
                        //  leading to a bad message being sent.
                        // To get around this LongPolling will not break up the message.
                        //options.Transports = HttpTransportType.LongPolling;
                    }
                )
                .Build();

            _connection.Closed += HandleConnectionClosed;
            await _connection.StartAsync(cancellationToken);
            _logger.LogInformation("Connection State: " + _connection.State);
            Api = new SignalrZoneEditorApi(
                _loggerFactory.CreateLogger<SignalrZoneEditorApi>(),
                _connection
            );
            await _mediator.Publish(
                new ZoneEditorServiceConnectedEvent(_currentZoneId),
                cancellationToken
            );
            return new();
        }
        catch (HttpRequestException ex)
        {
            await LogAndDispose(
                ex,
                "Failed to start connection to Zone Editor."
            );
            if (ex.Message.Contains("401 (Unauthorized)"))
            {
                await _mediator.Publish(
                    new ConnectionUnauthorizedEvent(zoneDetails.Id),
                    cancellationToken
                );
                return new(ConnectionErrorTypes.Unauthorized);
            }
            return new(ConnectionErrorTypes.Unknown);
        }
        catch (InvalidOperationException ex)
        {
            await LogAndDispose(
                ex,
                "Operation Exception starting connection to Zone Editor Service."
            );
            return new(ConnectionErrorTypes.Operational);
        }
        catch (Exception ex)
        {
            await LogAndDispose(
                ex,
                "Generic Exception starting connection to Zone Editor Service."
            );
            return new(ConnectionErrorTypes.Unknown);
        }
    }

    private Task HandleConnectionClosed(Exception? ex)
    {
        _logger.LogInformation(ex, "Connection Lost");
        _connection = null;
        _initialized = false;
        _currentZoneId = string.Empty;
        Api = new SignalrZoneEditorApi(
            _loggerFactory.CreateLogger<SignalrZoneEditorApi>(),
            null
        );

        return Task.CompletedTask;
    }

    public async Task<StandardCommandResult> Disconnect()
    {
        if (_connection != null)
        {
            await _connection.DisposeAsync();
            _connection = null;
            Api = new SignalrZoneEditorApi(
                _loggerFactory.CreateLogger<SignalrZoneEditorApi>(),
                null
            );
        }
        _initialized = false;
        _currentZoneId = string.Empty;

        return new();
    }

    private async Task LogAndDispose(Exception ex, string message)
    {
        _logger.LogWarning(ex, message);
        if (_connection != null)
        {
            await _connection.DisposeAsync();
            _connection = null;
        }
        _initialized = false;
        _currentZoneId = string.Empty;
    }
}
