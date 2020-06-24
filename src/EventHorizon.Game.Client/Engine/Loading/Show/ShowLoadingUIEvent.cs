using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

namespace EventHorizon.Game.Client.Engine.Loading.Show
{
    public struct ShowLoadingUIEvent : INotification
    {

    }

    public interface ShowLoadingUIEventObserver
        : ArgumentObserver<ShowLoadingUIEvent>
    {
    }

    public class ShowLoadingUIEventHandler
        : INotificationHandler<ShowLoadingUIEvent>
    {
        private readonly ObserverState _observer;

        public ShowLoadingUIEventHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            ShowLoadingUIEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<ShowLoadingUIEventObserver, ShowLoadingUIEvent>(
            notification,
            cancellationToken
        );
    }
}
