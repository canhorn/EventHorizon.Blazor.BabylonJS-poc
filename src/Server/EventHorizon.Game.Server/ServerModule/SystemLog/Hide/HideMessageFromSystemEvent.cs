namespace EventHorizon.Game.Server.ServerModule.SystemLog.Hide
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Observer.State;
    using MediatR;

    public class HideMessageFromSystemEventHandler
        : INotificationHandler<HideMessageFromSystemEvent>
    {
        private readonly ObserverState _observer;

        public HideMessageFromSystemEventHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            HideMessageFromSystemEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<HideMessageFromSystemEventObserver, HideMessageFromSystemEvent>(
            notification,
            cancellationToken
        );
    }
}
