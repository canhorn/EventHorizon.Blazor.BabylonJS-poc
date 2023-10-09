namespace EventHorizon.Game.Client.Systems.Local.InView.Entering;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;

using MediatR;

public struct EntityEnteringViewEvent : INotification
{
    public long ClientId { get; }

    public EntityEnteringViewEvent(long clientId)
    {
        ClientId = clientId;
    }
}

public interface EntityEnteringViewEventObserver
    : ArgumentObserver<EntityEnteringViewEvent> { }

public class EntityEnteringViewEventHandler
    : INotificationHandler<EntityEnteringViewEvent>
{
    private readonly ObserverState _observer;

    public EntityEnteringViewEventHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        EntityEnteringViewEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            EntityEnteringViewEventObserver,
            EntityEnteringViewEvent
        >(notification, cancellationToken);
}
