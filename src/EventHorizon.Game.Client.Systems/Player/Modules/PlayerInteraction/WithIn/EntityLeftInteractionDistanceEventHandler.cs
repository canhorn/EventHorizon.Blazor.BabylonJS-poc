namespace EventHorizon.Game.Client.Systems.Player.Modules.PlayerInteraction.WithIn;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.State;
using MediatR;

public class EntityLeftInteractionDistanceEventHandler
    : INotificationHandler<EntityLeftInteractionDistanceEvent>
{
    private readonly ObserverState _observer;

    public EntityLeftInteractionDistanceEventHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        EntityLeftInteractionDistanceEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            EntityLeftInteractionDistanceEventObserver,
            EntityLeftInteractionDistanceEvent
        >(notification, cancellationToken);
}
