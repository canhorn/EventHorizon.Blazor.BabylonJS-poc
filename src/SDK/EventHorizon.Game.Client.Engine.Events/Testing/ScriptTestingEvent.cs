namespace EventHorizon.Game.Client.Engine.Events.Testing
{
    using EventHorizon.Observer.Model;
    using MediatR;

    public struct ScriptTestingEvent 
        : INotification
    {

    }

    public interface ScriptTestingEventObserver
        : ArgumentObserver<ScriptTestingEvent>
    {
    }
}
