namespace EventHorizon.Game.Client.Systems.Entity.Stopping;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

public struct EntityStoppingEvent : INotification
{
    public long ClientId { get; }

    public EntityStoppingEvent(long clientId)
    {
        ClientId = clientId;
    }
}

public interface EntityStoppingEventObserver : ArgumentObserver<EntityStoppingEvent> { }

public class EntityStoppingEventHandler : INotificationHandler<EntityStoppingEvent>
{
    private readonly ObserverState _observer;

    public EntityStoppingEventHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(EntityStoppingEvent notification, CancellationToken cancellationToken) =>
        _observer.Trigger<EntityStoppingEventObserver, EntityStoppingEvent>(
            notification,
            cancellationToken
        );
}
