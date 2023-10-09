namespace EventHorizon.Game.Client.Systems.Player.Modules.Input.Move;

using EventHorizon.Game.Client.Systems.Player.Modules.Input.Model;
using EventHorizon.Observer.Model;

using MediatR;

public struct MovePlayerInDirectionEvent : INotification
{
    public MoveDirection Direction { get; }

    public MovePlayerInDirectionEvent(MoveDirection direction)
    {
        Direction = direction;
    }
}

public interface MovePlayerInDirectionEventObserver
    : ArgumentObserver<MovePlayerInDirectionEvent> { }
