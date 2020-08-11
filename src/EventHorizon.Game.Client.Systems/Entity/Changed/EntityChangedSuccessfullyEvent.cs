namespace EventHorizon.Game.Client.Systems.Entity.Changed
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    public struct EntityChangedSuccessfullyEvent : INotification
    {
        public long EntityId { get; }

        public EntityChangedSuccessfullyEvent(
            long entityId
        )
        {
            EntityId = entityId;
        }
    }

    public interface EntityChangedSuccessfullyEventObserver
        : ArgumentObserver<EntityChangedSuccessfullyEvent>
    {
    }

    public class EntityChangedSuccessfullyEventHandler
        : INotificationHandler<EntityChangedSuccessfullyEvent>
    {
        private readonly ObserverState _observer;

        public EntityChangedSuccessfullyEventHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            EntityChangedSuccessfullyEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<EntityChangedSuccessfullyEventObserver, EntityChangedSuccessfullyEvent>(
            notification,
            cancellationToken
        );
    }
}
