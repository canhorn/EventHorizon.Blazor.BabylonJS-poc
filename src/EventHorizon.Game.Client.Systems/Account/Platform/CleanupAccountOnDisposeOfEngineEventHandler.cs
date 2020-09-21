namespace EventHorizon.Game.Client.Systems.Account.Platform
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Lifecycle.Dispose;
    using EventHorizon.Game.Client.Systems.Account.Api;
    using MediatR;

    public class CleanupAccountOnDisposeOfEngineEventHandler
        : INotificationHandler<DisposeOfEngineEvent>
    {
        private readonly IAccountState _state;

        public CleanupAccountOnDisposeOfEngineEventHandler(
            IAccountState state
        )
        {
            _state = state;
        }

        public Task Handle(
            DisposeOfEngineEvent notification, 
            CancellationToken cancellationToken
        )
        {
            _state.Reset();

            return Task.CompletedTask;
        }
    }
}
