namespace EventHorizon.Game.Client.Systems.Map.Ready
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    public struct MapMeshReadyEvent 
        : INotification
    {

    }

    public interface MapMeshReadyEventObserver
        : ArgumentObserver<MapMeshReadyEvent>
    {
    }

    public class MapMeshReadyEventHandler
        : INotificationHandler<MapMeshReadyEvent>
    {
        private readonly ObserverState _observer;

        public MapMeshReadyEventHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            MapMeshReadyEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<MapMeshReadyEventObserver, MapMeshReadyEvent>(
            notification,
            cancellationToken
        );
    }
}
