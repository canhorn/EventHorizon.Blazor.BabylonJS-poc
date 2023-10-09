namespace EventHorizon.Game.Client.Engine.Testing.Events;

using EventHorizon.Observer.Model;

using MediatR;

// TODO: [TESTING] - Remove this when Not needed
public struct ScriptTestingEvent : INotification { }

public interface ScriptTestingEventObserver
    : ArgumentObserver<ScriptTestingEvent> { }
