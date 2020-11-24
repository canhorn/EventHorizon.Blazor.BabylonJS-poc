namespace EventHorizon.Game.Editor.Zone.Services.Service
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Connection;
    using EventHorizon.Game.Editor.Core.Services.Model;
    using EventHorizon.Game.Editor.Zone.Services.Api;
    using EventHorizon.Game.Editor.Zone.Services.Connection;
    using MediatR;
    using Microsoft.AspNetCore.SignalR.Client;
    using Microsoft.Extensions.Logging;

    public class SignalrZoneAdminServices
        : ZoneAdminServices
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public ZoneAdminApi Api { get; private set; } = new SignalrZoneAdminApi(null);

        private string _currentZoneId = string.Empty;
        private bool _initialized;
        private HubConnection _connection;

        public SignalrZoneAdminServices(
            ILogger<SignalrZoneAdminServices> logger,
            IMediator mediator
        )
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
            if (_initialized
                && zoneDetails.Id == _currentZoneId)
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
                    .WithUrl(
                        $"{zoneDetails.ServerAddress}/admin",
                        options =>
                        {
                            // options..LogLevel = SignalRLogLevel.Error;
                            options.AccessTokenProvider = () => Task.FromResult(
                                accessToken
                            );
                        }
                    ).Build();

                await _connection.StartAsync(cancellationToken);
                Api = new SignalrZoneAdminApi(
                    _connection
                );
                await _mediator.Publish(
                    new ZoneAdminServiceConnectedEvent(
                        _currentZoneId
                    ),
                    cancellationToken
                );
                return new();
            }
            catch (HttpRequestException ex)
            {
                await LogAndDispose(
                    ex,
                    "Failed to start connection to Zone Admin."
                );
                if (ex.Message.Contains("401 (Unauthorized)"))
                {
                    await _mediator.Publish(
                        new ConnectionUnauthorizedEvent(
                            zoneDetails.Id
                        ),
                        cancellationToken
                    );
                    return new(
                        ConnectionErrorTypes.Unauthorized
                    );
                }
                return new(
                    ConnectionErrorTypes.Unknown
                );
            }
            catch (InvalidOperationException ex)
            {
                await LogAndDispose(
                    ex,
                    "Operation Exception starting connection to Zone Admin Service."
                );
                return new(
                    ConnectionErrorTypes.Operational
                );
            }
            catch (Exception ex)
            {
                await LogAndDispose(
                    ex,
                    "Generic Exception starting connection to Zone Admin Service."
                );
                return new(
                    ConnectionErrorTypes.Unknown
                );
            }
        }

        public async Task<StandardCommandResult> Disconnect()
        {
            if (_connection != null)
            {
                await _connection.DisposeAsync();
                _connection = null;
            }
            _initialized = false;
            _currentZoneId = string.Empty;

            return new();
        }

        private async Task LogAndDispose(
            Exception ex,
            string message
        )
        {
            _logger.LogWarning(
                ex,
                message
            );
            if (_connection != null)
            {
                await _connection.DisposeAsync();
                _connection = null;
            }
            _initialized = false;
            _currentZoneId = string.Empty;
        }
    }
}
