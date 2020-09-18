namespace EventHorizon.Game.Client.Systems.Local.Modules.MeshManagement.Set
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Observer.State;
    using MediatR;

    public class MeshSetEventObserverHandler
        : INotificationHandler<MeshSetEvent>
    {
        private readonly ObserverState _observer;

        public MeshSetEventObserverHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            MeshSetEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<MeshSetEventObserver, MeshSetEvent>(
            notification,
            cancellationToken
        );
    }
}
