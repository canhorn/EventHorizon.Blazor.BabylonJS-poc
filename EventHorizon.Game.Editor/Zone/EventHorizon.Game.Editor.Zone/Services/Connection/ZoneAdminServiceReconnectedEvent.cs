namespace EventHorizon.Game.Editor.Zone.Services.Connection
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    public struct ZoneAdminServiceReconnectedEvent
        : INotification
    {
        public string ZoneId { get; }

        public ZoneAdminServiceReconnectedEvent(string zoneId)
        {
            ZoneId = zoneId;
        }
    }

    public interface ZoneAdminServiceReconnectedEventObserver
        : ArgumentObserver<ZoneAdminServiceReconnectedEvent>
    {
    }

    public class ZoneAdminServiceReconnectedEventObserverHandler
        : INotificationHandler<ZoneAdminServiceReconnectedEvent>
    {
        private readonly ObserverState _observer;

        public ZoneAdminServiceReconnectedEventObserverHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            ZoneAdminServiceReconnectedEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<ZoneAdminServiceReconnectedEventObserver, ZoneAdminServiceReconnectedEvent>(
            notification,
            cancellationToken
        );
    }
}
