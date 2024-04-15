namespace EventHorizon.Game.Server.ServerModule.Game.Updated
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    public struct GameStateUpdatedEvent : INotification { }

    public interface GameStateUpdatedEventObserver : ArgumentObserver<GameStateUpdatedEvent> { }

    public class GameStateUpdatedEventHandler : INotificationHandler<GameStateUpdatedEvent>
    {
        private readonly ObserverState _observer;

        public GameStateUpdatedEventHandler(ObserverState observer)
        {
            _observer = observer;
        }

        public Task Handle(
            GameStateUpdatedEvent notification,
            CancellationToken cancellationToken
        ) =>
            _observer.Trigger<GameStateUpdatedEventObserver, GameStateUpdatedEvent>(
                notification,
                cancellationToken
            );
    }
}
