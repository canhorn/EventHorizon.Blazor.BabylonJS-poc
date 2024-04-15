namespace EventHorizon.Game.Server.ServerModule.SystemLog.Hide
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    public struct HideMessageFromSystemEvent : INotification { }

    public interface HideMessageFromSystemEventObserver
        : ArgumentObserver<HideMessageFromSystemEvent> { }

    // TODO: Move this into an Implementation Project, Remove from the SDK
    public class HideMessageFromSystemEventHandler
        : INotificationHandler<HideMessageFromSystemEvent>
    {
        private readonly ObserverState _observer;

        public HideMessageFromSystemEventHandler(ObserverState observer)
        {
            _observer = observer;
        }

        public Task Handle(
            HideMessageFromSystemEvent notification,
            CancellationToken cancellationToken
        ) =>
            _observer.Trigger<HideMessageFromSystemEventObserver, HideMessageFromSystemEvent>(
                notification,
                cancellationToken
            );
    }
}
