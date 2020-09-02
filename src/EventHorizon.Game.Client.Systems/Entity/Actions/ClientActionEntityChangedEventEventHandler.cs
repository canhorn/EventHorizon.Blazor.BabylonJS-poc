namespace EventHorizon.Game.Client.Systems.Entity.Actions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Observer.State;
    using MediatR;

    public class ClientActionEntityChangedEventEventHandler
        : INotificationHandler<ClientActionEntityClientChangedEvent>
    {
        private readonly ObserverState _observer;

        public ClientActionEntityChangedEventEventHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            ClientActionEntityClientChangedEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<ClientActionEntityChangedEventObserver, ClientActionEntityClientChangedEvent>(
            notification,
            cancellationToken
        );
    }
}
