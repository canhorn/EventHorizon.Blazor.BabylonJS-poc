namespace EventHorizon.Game.Server.Actions.Agent
{
    using EventHorizon.Observer.Model;
    using MediatR;

    public struct ClearPointerHitEntityEvent 
        : INotification
    {

    }

    public interface ClearPointerHitEntityEventObserver
        : ArgumentObserver<ClearPointerHitEntityEvent>
    {
    }
}
