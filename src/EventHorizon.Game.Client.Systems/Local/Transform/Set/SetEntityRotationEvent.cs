namespace EventHorizon.Game.Client.Systems.Local.Transform.Set
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Entity.Api;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    public struct SetEntityRotationEvent : INotification
    {
        public long ClientId { get; }
        public IVector3 Rotation { get; }

        public SetEntityRotationEvent(
            long clientId,
            IVector3 rotation
        )
        {
            ClientId = clientId;
            Rotation = rotation;
        }
    }

    public interface SetEntityRotationEventObserver
        : ArgumentObserver<SetEntityRotationEvent>
    {
    }

    public class SetEntityRotationEventHandler
        : INotificationHandler<SetEntityRotationEvent>
    {
        private readonly ObserverState _observer;

        public SetEntityRotationEventHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            SetEntityRotationEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<SetEntityRotationEventObserver, SetEntityRotationEvent>(
            notification,
            cancellationToken
        );
    }
}
