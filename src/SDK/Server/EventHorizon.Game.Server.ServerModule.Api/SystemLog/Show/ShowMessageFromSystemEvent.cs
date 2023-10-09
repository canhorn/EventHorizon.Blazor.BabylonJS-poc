namespace EventHorizon.Game.Server.ServerModule.SystemLog.Show
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    public struct ShowMessageFromSystemEvent : INotification { }

    public interface ShowMessageFromSystemEventObserver
        : ArgumentObserver<ShowMessageFromSystemEvent> { }

    // TODO: Move this into an Implementation Project, Remove from the SDK
    public class ShowMessageFromSystemEventHandler
        : INotificationHandler<ShowMessageFromSystemEvent>
    {
        private readonly ObserverState _observer;

        public ShowMessageFromSystemEventHandler(ObserverState observer)
        {
            _observer = observer;
        }

        public Task Handle(
            ShowMessageFromSystemEvent notification,
            CancellationToken cancellationToken
        ) =>
            _observer.Trigger<
                ShowMessageFromSystemEventObserver,
                ShowMessageFromSystemEvent
            >(notification, cancellationToken);
    }
}
