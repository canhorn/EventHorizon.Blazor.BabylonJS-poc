namespace EventHorizon.Game.Client.Engine.Debugging.Enable;

using System;
using EventHorizon.Observer.Model;
using MediatR;

public record EnableEngineDebuggingEvent : INotification { }

public interface EnableEngineDebuggingEventObserver
    : ArgumentObserver<EnableEngineDebuggingEvent> { }
