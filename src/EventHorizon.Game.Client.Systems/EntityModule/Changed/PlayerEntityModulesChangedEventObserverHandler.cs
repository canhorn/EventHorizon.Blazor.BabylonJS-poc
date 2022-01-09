namespace EventHorizon.Game.Client.Systems.EntityModule.Register;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Observer.State;

using MediatR;

public class PlayerEntityModulesChangedEventObserverHandler
    : INotificationHandler<PlayerEntityModulesChangedEvent>
{
    private readonly ObserverState _observer;

    public PlayerEntityModulesChangedEventObserverHandler(
        ObserverState observer
    )
    {
        _observer = observer;
    }

    public Task Handle(
        PlayerEntityModulesChangedEvent notification,
        CancellationToken cancellationToken
    ) => _observer.Trigger<PlayerEntityModulesChangedEventObserver, PlayerEntityModulesChangedEvent>(
        notification,
        cancellationToken
    );
}
