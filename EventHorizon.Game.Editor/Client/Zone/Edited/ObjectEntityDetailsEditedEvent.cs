namespace EventHorizon.Game.Editor.Client.Zone.Edited;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

public struct ObjectEntityDetailsEditedEvent : INotification
{
    public IObjectEntityDetails Entity { get; }

    public ObjectEntityDetailsEditedEvent(IObjectEntityDetails entity)
    {
        Entity = entity;
    }
}

public interface ObjectEntityDetailsEditedEventObserver
    : ArgumentObserver<ObjectEntityDetailsEditedEvent> { }

public class ObjectEntityDetailsEditedEventObserverHandler
    : INotificationHandler<ObjectEntityDetailsEditedEvent>
{
    private readonly ObserverState _observer;

    public ObjectEntityDetailsEditedEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        ObjectEntityDetailsEditedEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<ObjectEntityDetailsEditedEventObserver, ObjectEntityDetailsEditedEvent>(
            notification,
            cancellationToken
        );
}
