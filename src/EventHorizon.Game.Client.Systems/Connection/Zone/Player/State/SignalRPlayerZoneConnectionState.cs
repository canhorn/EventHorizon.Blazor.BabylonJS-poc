﻿namespace EventHorizon.Game.Client.Systems.Connection.Zone.Player.State
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Publish;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Api;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Disconnected;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Info;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Model;
    using MediatR;
    using Microsoft.AspNetCore.SignalR.Client;
    using Microsoft.Extensions.Logging;

    public class SignalRPlayerZoneConnectionState
        : IPlayerZoneConnectionState
    {
        private HubConnection? _connection;

        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public bool IsConnected => _connection?.State == HubConnectionState.Connected;

        public SignalRPlayerZoneConnectionState(
            ILogger<SignalRPlayerZoneConnectionState> logger,
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
                    ).ConfigureLogging(
                        builder =>
                        {
                            builder.AddProvider(GameServiceProvider.GetService<ILoggerProvider>());
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
                                new PlayerZoneInfoReceivedEvent(
                                    zoneInfo
                                )
                            );

                            // Published that the PlayerZoneInfoReceivedEvent has finished and setup to receive events.
                            // TODO: [CLEANUP] : Replaces "gameLoadedEvent" & "createZoneLoadedEvent"
                            await _mediator.Publish(
                                new FinishedPlayerZoneInfoReceivedEvent()
                            );
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(
                                ex,
                                "Player Zone Connection ZoneInfo Failed"
                            );
                        }
                    }
                );

                // Setup ClientAction Handler
                _connection.On<string, IDictionary<string, object>>(
                    "ClientAction",
                    async (actionName, data) =>
                    {
                        // TODO: [ClientAction] : Publish on Received From Server
                        await _mediator.Send(
                            new PublishClientActionCommand(
                                actionName,
                                data
                            )
                        );
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
                return _mediator.Publish(
                    new PlayerZoneConnectionDisconnectedEvent(
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

            return _mediator.Publish(
                new PlayerZoneConnectionDisconnectedEvent(
                    code,
                    ex
                )
            );
        }

        public Task StopConnection()
        {
            if (_connection == null)
            {
                return Task.CompletedTask;
            }
            return _connection.StopAsync();
        }

        public async Task InvokeMethod(
            string methodName,
            IList<object> data
        )
        {
            if (_connection?.State != HubConnectionState.Connected)
            {
                return;
            }
            await _connection.InvokeCoreAsync(
                methodName,
                data.ToArray()
            );
        }
    }
}