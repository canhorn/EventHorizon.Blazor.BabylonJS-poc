namespace EventHorizon.Game.Client.Systems.Connection.Core.State
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Systems.Connection.Core.Account.Connected;
    using EventHorizon.Game.Client.Systems.Connection.Core.Account.Disconnected;
    using EventHorizon.Game.Client.Systems.Connection.Core.Api;
    using EventHorizon.Game.Client.Systems.Connection.Core.Model;
    using EventHorizon.Game.Client.Systems.Connection.HubBuilder;
    using MediatR;
    using Microsoft.AspNetCore.Connections;
    using Microsoft.AspNetCore.Http.Connections;
    using Microsoft.AspNetCore.SignalR.Client;
    using Microsoft.AspNetCore.SignalR.Protocol;
    using Microsoft.Extensions.Logging;

    public class SignalRCoreConnectionState
        : ICoreConnectionState
    {
        private HubConnection? _connection;

        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public bool IsConnected => _connection?.State == HubConnectionState.Connected;

        public SignalRCoreConnectionState(
            ILogger<SignalRCoreConnectionState> logger,
            IMediator mediator
        )
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task StartConnection(
            string serverUrl,
            string accessToken
        )
        {
            if (_connection != null)
            {
                return;
            }
            try
            {
                _connection = new HubConnectionBuilder()
                    .WithUrl(
                        new Uri(
                            $"{serverUrl}/coreBus"
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
                    ).WithAutomaticReconnect()
                    .Build();

                _connection.On(
                    "AccountConnected",
                    async (AccountInfoModel accountInfo) =>
                    {
                        // TODO: [DEBUGGING] : Track Debugging Object Command
                        //await _mediator.Send(
                        //    new TrackDebuggingObjectCommand(
                        //        "AccountDetails",
                        //        accountInfo
                        //    )
                        //);
                        await _mediator.Publish(
                            new AccountConnectedEvent(
                                accountInfo
                            )
                        );
                    }
                );

                _connection.Closed += HandleConnectionClosed;

                await _connection.StartAsync();
            }
            catch (Exception ex)
            {
                _connection = null;
                _logger.LogError(
                    ex,
                    "Core Connection Start Failed"
                );
                //await _mediator.Publish(
                //     new AccountDisconnectedEvent(
                //         "status_code_401",
                //         ex
                //     )
                // );
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
                new AccountDisconnectedEvent(
                    code,
                    ex
                )
            );
        }

        public Task StopConnection()
        {
            return _connection?.StopAsync()
                ?? Task.CompletedTask;
        }
    }
}
