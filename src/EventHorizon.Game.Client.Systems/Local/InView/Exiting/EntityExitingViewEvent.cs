namespace EventHorizon.Game.Client.Systems.Local.InView.Exiting
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    public struct EntityExitingViewEvent : INotification
    {
        public long ClientId { get; }

        public EntityExitingViewEvent(
            long clientId
        )
        {
            ClientId = clientId;
        }
    }

    public interface EntityExitingViewEventObserver
        : ArgumentObserver<EntityExitingViewEvent>
    {
    }

    public class EntityExitingViewEventHandler
        : INotificationHandler<EntityExitingViewEvent>
    {
        private readonly ObserverState _observer;

        public EntityExitingViewEventHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            EntityExitingViewEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<EntityExitingViewEventObserver, EntityExitingViewEvent>(
            notification,
            cancellationToken
        );
    }
}
