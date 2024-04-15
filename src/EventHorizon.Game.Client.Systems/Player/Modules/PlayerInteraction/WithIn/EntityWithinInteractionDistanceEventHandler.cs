namespace EventHorizon.Game.Client.Systems.Player.Modules.PlayerInteraction.WithIn;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.State;
using MediatR;

public class EntityWithinInteractionDistanceEventHandler
    : INotificationHandler<EntityWithinInteractionDistanceEvent>
{
    private readonly ObserverState _observer;

    public EntityWithinInteractionDistanceEventHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        EntityWithinInteractionDistanceEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            EntityWithinInteractionDistanceEventObserver,
            EntityWithinInteractionDistanceEvent
        >(notification, cancellationToken);
}
