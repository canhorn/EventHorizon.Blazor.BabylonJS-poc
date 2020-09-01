namespace EventHorizon.Game.Client.Engine.Testing.Events
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
