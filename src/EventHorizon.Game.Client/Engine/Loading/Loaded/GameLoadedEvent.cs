using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

namespace EventHorizon.Game.Client.Engine.Loading.Loaded
{
    public struct GameLoadedEvent : INotification
    {

    }

    public interface GameLoadedEventObserver
        : ArgumentObserver<GameLoadedEvent>
    {
    }

    public class GameLoadedEventHandler
        : INotificationHandler<GameLoadedEvent>
    {
        private readonly ObserverState _observer;

        public GameLoadedEventHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            GameLoadedEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<GameLoadedEventObserver, GameLoadedEvent>(
            notification,
            cancellationToken
        );
    }
}
