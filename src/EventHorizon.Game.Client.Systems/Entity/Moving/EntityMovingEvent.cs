namespace EventHorizon.Game.Client.Systems.Entity.Moving;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

public struct EntityMovingEvent : INotification
{
    public long ClientId { get; }

    public EntityMovingEvent(long clientId)
    {
        ClientId = clientId;
    }
}

public interface EntityMovingEventObserver : ArgumentObserver<EntityMovingEvent> { }

public class EntityMovingEventHandler : INotificationHandler<EntityMovingEvent>
{
    private readonly ObserverState _observer;

    public EntityMovingEventHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(EntityMovingEvent notification, CancellationToken cancellationToken) =>
        _observer.Trigger<EntityMovingEventObserver, EntityMovingEvent>(
            notification,
            cancellationToken
        );
}
