namespace EventHorizon.Game.Server.ClientAction.Agent;

using EventHorizon.Observer.Model;

using MediatR;

public struct ClearPointerHitEntityEvent : INotification { }

public interface ClearPointerHitEntityEventObserver
    : ArgumentObserver<ClearPointerHitEntityEvent> { }
