namespace EventHorizon.Game.Client.Systems.Player.Modules.Input.Move;

using EventHorizon.Game.Client.Engine.Input.Model;
using EventHorizon.Game.Client.Systems.Player.Modules.Input.Model;
using EventHorizon.Observer.Model;
using MediatR;

public record MovePlayerInDirectionEvent(MoveDirection Direction) : INotification;

public interface MovePlayerInDirectionEventObserver
    : ArgumentObserver<MovePlayerInDirectionEvent> { }
