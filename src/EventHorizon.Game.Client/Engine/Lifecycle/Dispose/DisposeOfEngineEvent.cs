namespace EventHorizon.Game.Client.Engine.Lifecycle.Dispose;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

public struct DisposeOfEngineEvent : INotification { }

public interface DisposeOfEngineEventObserver : ArgumentObserver<DisposeOfEngineEvent> { }

public class DisposeOfEngineEventObserverHandler : INotificationHandler<DisposeOfEngineEvent>
{
    private readonly ObserverState _observer;

    public DisposeOfEngineEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(DisposeOfEngineEvent notification, CancellationToken cancellationToken) =>
        _observer.Trigger<DisposeOfEngineEventObserver, DisposeOfEngineEvent>(
            notification,
            cancellationToken
        );
}
