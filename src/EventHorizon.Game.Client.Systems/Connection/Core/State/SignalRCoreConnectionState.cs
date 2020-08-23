namespace EventHorizon.Game.Client.Systems.Connection.Core.State
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Systems.Connection.Core.Account.Connected;
    using EventHorizon.Game.Client.Systems.Connection.Core.Account.Disconnected;
    using EventHorizon.Game.Client.Systems.Connection.Core.Api;
    using EventHorizon.Game.Client.Systems.Connection.Core.Model;
    using MediatR;
    using Microsoft.AspNetCore.SignalR.Client;
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
                    ).Build();

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
                    new AccountDisconnectedEvent(
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
