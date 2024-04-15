namespace EventHorizon.Game.Editor.Zone.Services.Service;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Connection.Shared;
using EventHorizon.Connection.Shared.Unauthorized;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.Zone.Services.Command.Response;
using EventHorizon.Game.Editor.Core.Services.Model;
using EventHorizon.Game.Editor.Services.Model.Command;
using EventHorizon.Game.Editor.Zone.AdminClientAction.Publish;
using EventHorizon.Game.Editor.Zone.Services.Api;
using EventHorizon.Game.Editor.Zone.Services.Connection;
using MediatR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

public class SignalrZoneAdminServices : ZoneAdminServices
{
    private readonly ILogger _logger;
    private readonly IMediator _mediator;

    public ZoneAdminApi Api { get; private set; } = new SignalrZoneAdminApi(null);

    private string _currentZoneId = string.Empty;
    private bool _initialized;
    private HubConnection? _connection;

    public SignalrZoneAdminServices(ILogger<SignalrZoneAdminServices> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
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
                Api = new SignalrZoneAdminApi(null);
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
                    $"{zoneDetails.ServerAddress}/admin",
                    options =>
                    {
                        // options..LogLevel = SignalRLogLevel.Error;
                        options.AccessTokenProvider = () => Task.FromResult<string?>(accessToken);
                    }
                )
                .Build();

            SetupEvents();
            _connection.Closed += HandleClosed;
            _connection.Reconnecting += HandleReconnecting;
            _connection.Reconnected += HandleReconnected;

            await _connection.StartAsync(cancellationToken);
            Api = new SignalrZoneAdminApi(_connection);
            await _mediator.Publish(
                new ZoneAdminServiceConnectedEvent(_currentZoneId),
                cancellationToken
            );
            return new();
        }
        catch (HttpRequestException ex)
        {
            await LogAndDispose(
                ex,
                "Failed to start connection to Zone Admin.",
                ZoneAdminConnectionCodes.HTTP_REQUEST_FAILURE
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
                "Operation Exception starting connection to Zone Admin Service.",
                ZoneAdminConnectionCodes.INVALID_OPERATION_FAILURE
            );
            return new(ConnectionErrorTypes.Operational);
        }
        catch (Exception ex)
        {
            await LogAndDispose(
                ex,
                "Generic Exception starting connection to Zone Admin Service.",
                ZoneAdminConnectionCodes.GENERAL_FAILURE
            );
            return new(ConnectionErrorTypes.Unknown);
        }
    }

    private async Task HandleClosed(Exception? ex)
    {
        await LogAndDispose(ex, "Connection Closed", ZoneAdminConnectionCodes.CLOSED_BY_CONNECTION);
    }

    private Task HandleReconnecting(Exception? ex)
    {
        _logger.LogWarning(ex, "Reconnecting triggered...");
        return _mediator.Publish(new ZoneAdminServiceReconnectingEvent(_currentZoneId));
    }

    private Task HandleReconnected(
        string? _ // connectionId
    )
    {
        return _mediator.Publish(new ZoneAdminServiceReconnectedEvent(_currentZoneId));
    }

    private void SetupEvents()
    {
        _connection?.On<AdminCommandResponse>(
            "AdminCommandResponse",
            response => _mediator.Publish(new AdminCommandResponseEvent(response))
        );

        _connection?.On<string, IDictionary<string, object>>(
            "AdminClientAction",
            async (actionName, data) =>
            {
                try
                {
                    await _mediator.Send(new PublishAdminClientActionCommand(actionName, data));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Admin Client Action Error: {ClientAction}", actionName);
                }
            }
        );
    }

    public async Task<StandardCommandResult> Disconnect()
    {
        await LogAndDispose(null, "Disconnected request", ZoneAdminConnectionCodes.EXPLICT_CLOSED);

        return new();
    }

    private async Task LogAndDispose(Exception? ex, string message, string disposeReason)
    {
        var currentZoneId = _currentZoneId;
        _logger.LogWarning(ex, message);
        if (_connection != null)
        {
            await _connection.DisposeAsync();
            _connection = null;
            Api = new SignalrZoneAdminApi(null);
        }
        _initialized = false;
        _currentZoneId = String.Empty;
        if (currentZoneId.IsNotNullOrEmpty())
        {
            // If _connection is not null, then it was closed by the connection
            await _mediator.Publish(
                new ZoneAdminServiceDisconnectedEvent(currentZoneId, disposeReason)
            );
        }
    }
}
