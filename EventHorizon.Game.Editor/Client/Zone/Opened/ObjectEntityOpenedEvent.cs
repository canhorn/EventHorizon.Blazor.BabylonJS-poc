namespace EventHorizon.Game.Editor.Client.Zone.Opened
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    public struct ObjectEntityOpenedEvent
        : INotification
    {
        public string ObjectEntityId { get; }

        public ObjectEntityOpenedEvent(
            string objectEntityId
        )
        {
            ObjectEntityId = objectEntityId;
        }
    }

    public interface ObjectEntityOpenedEventObserver
        : ArgumentObserver<ObjectEntityOpenedEvent>
    {
    }

    public class ObjectEntityOpenedEventObserverHandler
        : INotificationHandler<ObjectEntityOpenedEvent>
    {
        private readonly ObserverState _observer;

        public ObjectEntityOpenedEventObserverHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            ObjectEntityOpenedEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<ObjectEntityOpenedEventObserver, ObjectEntityOpenedEvent>(
            notification,
            cancellationToken
        );
    }
}
