namespace EventHorizon.Game.Editor.Core.Services.Service;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Connection.Shared;
using EventHorizon.Connection.Shared.Unauthorized;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Core.Services.Api;
using EventHorizon.Game.Editor.Core.Services.Connection;
using EventHorizon.Game.Editor.Core.Services.Model;
using EventHorizon.Game.Editor.Core.Services.Registered;
using EventHorizon.Game.Editor.Model;
using MediatR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

public class SignalrCoreAdminServices : CoreAdminServices, IDisposable
{
    private static IList<CoreZoneDetails> EMPTY_LIST { get; } =
        new List<CoreZoneDetails>().AsReadOnly();

    private readonly ILogger _logger;
    private readonly IMediator _mediator;
    private readonly GamePlatformServiceSettings _settings;

    private bool _initializing = false;
    private bool _initialized = false;
    private HubConnection? _connection;

    public SignalrCoreAdminServices(
        ILogger<SignalrCoreAdminServices> logger,
        IMediator mediator,
        GamePlatformServiceSettings settings
    )
    {
        _logger = logger;
        _mediator = mediator;
        _settings = settings;
    }

    public async Task<StandardCommandResult> Connect(
        string accessToken,
        CancellationToken cancellationToken
    )
    {
        if (_initializing)
        {
            return new(ConnectionErrorTypes.NotInitialized);
        }
        else if (_initialized)
        {
            return new();
        }
        try
        {
            _initializing = true;
            _connection = new HubConnectionBuilder()
                .WithUrl(
                    $"{_settings.CoreServer}/admin",
                    options =>
                    {
                        // options..LogLevel = SignalRLogLevel.Error;
                        options.AccessTokenProvider = () => accessToken.FromResult<string?>();
                    }
                )
                .Build();

            await _connection.StartAsync(cancellationToken);
            SetupEventListeners(_connection);

            _initializing = false;
            _initialized = true;
            await _mediator.Publish(new CoreAdminServiceConnected(), cancellationToken);
            return new();
        }
        catch (HttpRequestException ex)
        {
            await LogAndDispose(ex, "Failed to start connection to Core Admin Service.");
            if (ex.Message.Contains("401 (Unauthorized)"))
            {
                await _mediator.Publish(
                    new ConnectionUnauthorizedEvent("core-admin"),
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
                "Operation Exception starting connection to Core Admin Service."
            );
            return new(ConnectionErrorTypes.Operational);
        }
        catch (Exception ex)
        {
            await LogAndDispose(ex, "Generic Exception starting connection to Core Admin Service.");
            return new(ConnectionErrorTypes.Unknown);
        }
    }

    private void SetupEventListeners(HubConnection connection)
    {
        connection.On(
            "ZoneRegistered",
            async (CoreZoneDetails zoneDetails) =>
            {
                await _mediator.Publish(new ZoneRegisteredOnCoreServer(zoneDetails));
            }
        );
        connection.On(
            "ZoneUnregistered",
            async (string zoneId) =>
            {
                await _mediator.Publish(new ZoneUnregisteredOnCoreServer(zoneId));
            }
        );
    }

    private async Task LogAndDispose(Exception ex, string message)
    {
        _logger.LogWarning(ex, message);
        if (_connection.IsNotNull())
        {
            await _connection.DisposeAsync();
            _connection = null;
        }
        _initializing = false;
        _initialized = false;
    }

    public void Dispose()
    {
        if (_connection.IsNotNull())
        {
            _ = _connection.DisposeAsync();
            _connection = null;
        }
        _initializing = false;
        _initialized = false;
    }

    public async Task<IList<CoreZoneDetails>> GetAllZones()
    {
        if (!_initialized || _initializing || _connection.IsNotConnected())
        {
            return EMPTY_LIST;
        }
        return await _connection.InvokeAsync<IList<CoreZoneDetails>>("GetAllZones");
    }
}
