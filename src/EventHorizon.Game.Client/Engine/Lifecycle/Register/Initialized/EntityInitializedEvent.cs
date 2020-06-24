namespace EventHorizon.Game.Client.Engine.Lifecycle.Register.Initialized
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Lifecycle.Model;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    public struct EntityInitializedEvent : INotification
    {
        public LifecycleEntityBase Entity { get; }

        public EntityInitializedEvent(
            LifecycleEntityBase entity
        )
        {
            Entity = entity;
        }
    }

    public interface EntityInitializedEventObserver
        : ArgumentObserver<EntityInitializedEvent>
    {
    }

    public class EntityInitializedEventHandler
        : INotificationHandler<EntityInitializedEvent>
    {
        private readonly ObserverState _observer;

        public EntityInitializedEventHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            EntityInitializedEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<EntityInitializedEventObserver, EntityInitializedEvent>(
            notification,
            cancellationToken
        );
    }
}
