namespace EventHorizon.Game.Client.Systems.Entity.Actions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Observer.State;
    using MediatR;

    public class ClientActionEntityStoppingEventHandler
        : INotificationHandler<ClientActionEntityStoppingEvent>
    {
        private readonly ObserverState _observer;

        public ClientActionEntityStoppingEventHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            ClientActionEntityStoppingEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<ClientActionEntityStoppingEventObserver, ClientActionEntityStoppingEvent>(
            notification,
            cancellationToken
        );
    }
}
