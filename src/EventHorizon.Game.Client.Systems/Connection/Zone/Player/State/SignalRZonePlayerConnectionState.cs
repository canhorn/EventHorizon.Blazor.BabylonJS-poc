namespace EventHorizon.Game.Client.Systems.Connection.Zone.Player.State
{
    using System;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Api;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Disconnected;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Info;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Model;
    using MediatR;
    using Microsoft.AspNetCore.SignalR.Client;
    using Microsoft.Extensions.Logging;

    public class SignalRZonePlayerConnectionState
        : IZonePlayerConnectionState
    {
        private HubConnection _connection;

        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public bool IsConnected => _connection?.State == HubConnectionState.Connected;

        public SignalRZonePlayerConnectionState(
            ILogger<SignalRZonePlayerConnectionState> logger,
            IMediator mediator
        )
        {
            _logger = logger;
            _mediator = mediator;
        }

        public Task StartConnection(
            string serverUrl,
            string accessToken
        )
        {
            if (_connection != null)
            {
                return Task.CompletedTask;
            }
            try
            {
                _connection = new HubConnectionBuilder()
                    .WithUrl(
                        new Uri(
                            $"{serverUrl}/playerHub"
                        ),
                        options =>
                        {
                            options.AccessTokenProvider = () => accessToken.FromResult();
                        }
                    ).Build();

                _connection.On(
                    "ZoneInfo",
                    async (PlayerZoneInfoModel zoneInfo) =>
                    {
                        try
                        {
                            _logger.LogDebug(
                                "ZoneInfo Received {Now}",
                                DateTime.UtcNow
                            );
                            // TODO: [DEBUGGING] : Track Debugging Object Command
                            //await _mediator.Send(
                            //    new TrackDebuggingObjectCommand(
                            //        "AccountDetails",
                            //        accountInfo
                            //    )
                            //);
                            await _mediator.Publish(
                                new ZonePlayerInfoReceivedEvent(
                                    zoneInfo
                                )
                            );

                            // Published that the ZonePlayerInfoReceivedEvent has finished and setup to receive events.
                            // TODO: [CLEANUP] : Replaces "gameLoadedEvent" & "createZoneLoadedEvent"
                            await _mediator.Publish(
                                new FinishedZonePlayerInfoReceivedEvent()
                            );

                            _logger.LogInformation("Hello");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"Error: {ex.GetType().Name} | {ex.Message}", ex);
                        }
                    }
                );

                // Setup ClientAction Handler
                _connection.On(
                    "ClientAction",
                    () =>
                    {
                        // TODO: [ClientAction] : Publish on Received From Server
                        // await _mediator.Publish(...);
                    }
                );

                _connection.Closed += HandleConnectionClosed;

                return _connection.StartAsync();
            }
            catch (Exception ex)
            {
                _connection = null;
                _logger.LogError(
                    "Core Connection Start Failed",
                    ex
                );
                return _mediator.Send(
                    new ZonePlayerConnectionDisconnectedEvent(
                        "status_code_401",
                        ex
                    )
                );
            }
        }

        private Task HandleConnectionClosed(
            Exception ex
        )
        {
            var code = "closed";
            if (ex != null)
            {
                _logger.LogError(
                    "Core Bus Closed, with Exception",
                    ex
                );
                code = "exception";
            }

            return _mediator.Send(
                new ZonePlayerConnectionDisconnectedEvent(
                    code,
                    ex
                )
            );
        }

        public Task StopConnection()
        {
            return _connection.StopAsync();
        }
    }
}
