namespace EventHorizon.Game.Client.Engine.Window.Resize;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

public struct SystemWindowResizedEvent : INotification { }

public interface SystemWindowResizedEventObserver : ArgumentObserver<SystemWindowResizedEvent> { }

public class SystemWindowResizedEventObserverHandler
    : INotificationHandler<SystemWindowResizedEvent>
{
    private readonly ObserverState _observer;

    public SystemWindowResizedEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        SystemWindowResizedEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<SystemWindowResizedEventObserver, SystemWindowResizedEvent>(
            notification,
            cancellationToken
        );
}
