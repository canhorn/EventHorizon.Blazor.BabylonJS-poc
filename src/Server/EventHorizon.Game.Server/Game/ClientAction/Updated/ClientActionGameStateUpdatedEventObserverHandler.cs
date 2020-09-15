namespace EventHorizon.Game.Server.Game.ClientAction.Updated
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Observer.State;
    using MediatR;

    public class ClientActionGameStateUpdatedEventObserverHandler
        : INotificationHandler<ClientActionGameStateUpdatedEvent>
    {
        private readonly ObserverState _observer;

        public ClientActionGameStateUpdatedEventObserverHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            ClientActionGameStateUpdatedEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<ClientActionGameStateUpdatedEventObserver, ClientActionGameStateUpdatedEvent>(
            notification,
            cancellationToken
        );
    }
}
