namespace EventHorizon.Game.Client.Systems.Entity.Changed;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.State;
using MediatR;

public class EntityChangedSuccessfullyEventHandler
    : INotificationHandler<EntityChangedSuccessfullyEvent>
{
    private readonly ObserverState _observer;

    public EntityChangedSuccessfullyEventHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        EntityChangedSuccessfullyEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<EntityChangedSuccessfullyEventObserver, EntityChangedSuccessfullyEvent>(
            notification,
            cancellationToken
        );
}
