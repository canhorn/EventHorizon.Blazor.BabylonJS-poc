namespace EventHorizon.Game.Client.Engine.Lifecycle.Register.Disposed;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Lifecycle.Api;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;

using MediatR;

public struct EntityDisposedEvent : INotification
{
    public IDisposableEntity Entity { get; }

    public EntityDisposedEvent(IDisposableEntity entity)
    {
        Entity = entity;
    }
}

public interface EntityDisposedEventObserver
    : ArgumentObserver<EntityDisposedEvent> { }

public class EntityDisposedEventHandler
    : INotificationHandler<EntityDisposedEvent>
{
    private readonly ObserverState _observer;

    public EntityDisposedEventHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        EntityDisposedEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<EntityDisposedEventObserver, EntityDisposedEvent>(
            notification,
            cancellationToken
        );
}
