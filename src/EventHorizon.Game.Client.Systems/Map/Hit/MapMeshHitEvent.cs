namespace EventHorizon.Game.Client.Systems.Map.Hit
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Entity.Api;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    public struct MapMeshHitEvent : INotification
    {
        public IVector3 Poisition { get; }

        public MapMeshHitEvent(
            IVector3 poisition
        )
        {
            Poisition = poisition;
        }
    }

    public interface MapMeshHitEventObserver
        : ArgumentObserver<MapMeshHitEvent>
    {
    }

    public class MapMeshHitEventHandler
        : INotificationHandler<MapMeshHitEvent>
    {
        private readonly ObserverState _observer;

        public MapMeshHitEventHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            MapMeshHitEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<MapMeshHitEventObserver, MapMeshHitEvent>(
            notification,
            cancellationToken
        );
    }
}
