namespace EventHorizon.Game.Editor.Client.Zone.Interaction;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Editor.Client.Zone.Model;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

public struct ObjectEntityInteractionEvent : INotification
{
    public string ObjectEntityId { get; }
    public EntityInteractionAction Action { get; }

    public ObjectEntityInteractionEvent(string objectEntityId, EntityInteractionAction action)
    {
        ObjectEntityId = objectEntityId;
        Action = action;
    }
}

public interface ObjectEntityOpenedEventObserver
    : ArgumentObserver<ObjectEntityInteractionEvent> { }

public class ObjectEntityOpenedEventObserverHandler
    : INotificationHandler<ObjectEntityInteractionEvent>
{
    private readonly ObserverState _observer;

    public ObjectEntityOpenedEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        ObjectEntityInteractionEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<ObjectEntityOpenedEventObserver, ObjectEntityInteractionEvent>(
            notification,
            cancellationToken
        );
}
