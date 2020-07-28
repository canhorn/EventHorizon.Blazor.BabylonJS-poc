namespace EventHorizon.Game.Client.Systems.Connection.Zone.Player.Info
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Api;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    public struct ZonePlayerInfoReceivedEvent : INotification
    {
        public IPlayerZoneInfo ZonePlayerInfo { get; }

        public ZonePlayerInfoReceivedEvent(
            IPlayerZoneInfo zonePlayerInfo
        )
        {
            ZonePlayerInfo = zonePlayerInfo;
        }
    }

    public interface ZonePlayerInfoReceivedEventObserver
        : ArgumentObserver<ZonePlayerInfoReceivedEvent>
    {
    }

    public class ZonePlayerInfoReceivedEventHandler
        : INotificationHandler<ZonePlayerInfoReceivedEvent>
    {
        private readonly ObserverState _observer;

        public ZonePlayerInfoReceivedEventHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            ZonePlayerInfoReceivedEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<ZonePlayerInfoReceivedEventObserver, ZonePlayerInfoReceivedEvent>(
            notification,
            cancellationToken
        );
    }
}
