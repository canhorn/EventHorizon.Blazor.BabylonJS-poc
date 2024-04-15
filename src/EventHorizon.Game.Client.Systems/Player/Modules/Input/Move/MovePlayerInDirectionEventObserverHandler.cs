namespace EventHorizon.Game.Client.Systems.Player.Modules.Input.Move;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.State;
using MediatR;

public class MovePlayerInDirectionEventObserverHandler
    : INotificationHandler<MovePlayerInDirectionEvent>
{
    private readonly ObserverState _observer;

    public MovePlayerInDirectionEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        MovePlayerInDirectionEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<MovePlayerInDirectionEventObserver, MovePlayerInDirectionEvent>(
            notification,
            cancellationToken
        );
}
