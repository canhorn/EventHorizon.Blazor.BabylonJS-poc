namespace EventHorizon.Game.Server.Game.Updated
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Observer.State;
    using MediatR;

    public class GameStateUpdatedEventHandler
        : INotificationHandler<GameStateUpdatedEvent>
    {
        private readonly ObserverState _observer;

        public GameStateUpdatedEventHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            GameStateUpdatedEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<GameStateUpdatedEventObserver, GameStateUpdatedEvent>(
            notification,
            cancellationToken
        );
    }
}
