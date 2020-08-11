namespace EventHorizon.Game.Client.Systems.Local.Modules.MeshManagement.Set
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    public struct MeshSetEvent : INotification
    {
        public long ClientId { get; }

        public MeshSetEvent(
            long clientId
        )
        {
            ClientId = clientId;
        }
    }

    public interface MeshSetEventObserver
        : ArgumentObserver<MeshSetEvent>
    {
    }

    public class MeshSetEventHandler
        : INotificationHandler<MeshSetEvent>
    {
        private readonly ObserverState _observer;

        public MeshSetEventHandler(
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
