namespace EventHorizon.Game.Editor.Client.Zone.Change
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    public struct ActiveZoneStateChangedEvent
        : INotification
    {
        public string ZoneId { get; }

        public ActiveZoneStateChangedEvent(
            string zoneId
        )
        {
            ZoneId = zoneId;
        }
    }

    public interface ActiveZoneStateChangedEventObserver
        : ArgumentObserver<ActiveZoneStateChangedEvent>
    {
    }

    public class ActiveZoneStateChangedEventObserverHandler
        : INotificationHandler<ActiveZoneStateChangedEvent>
    {
        private readonly ObserverState _observer;

        public ActiveZoneStateChangedEventObserverHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            ActiveZoneStateChangedEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<ActiveZoneStateChangedEventObserver, ActiveZoneStateChangedEvent>(
            notification,
            cancellationToken
        );
    }
}
