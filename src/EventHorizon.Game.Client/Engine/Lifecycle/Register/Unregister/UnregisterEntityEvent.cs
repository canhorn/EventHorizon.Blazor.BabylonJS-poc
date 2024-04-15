namespace EventHorizon.Game.Client.Engine.Lifecycle.Register.Unregister;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Lifecycle.Api;
using EventHorizon.Game.Client.Engine.Lifecycle.Model;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

public struct UnregisterEntityEvent : INotification
{
    public ILifecycleEntity Entity { get; }

    public UnregisterEntityEvent(ILifecycleEntity entity)
    {
        Entity = entity;
    }
}

public interface UnregisterEntityEventObserver : ArgumentObserver<UnregisterEntityEvent> { }

public class UnregisterEntityEventHandler : INotificationHandler<UnregisterEntityEvent>
{
    private readonly ObserverState _observer;

    public UnregisterEntityEventHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(UnregisterEntityEvent notification, CancellationToken cancellationToken) =>
        _observer.Trigger<UnregisterEntityEventObserver, UnregisterEntityEvent>(
            notification,
            cancellationToken
        );
}
