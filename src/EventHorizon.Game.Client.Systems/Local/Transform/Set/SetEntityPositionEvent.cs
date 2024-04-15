namespace EventHorizon.Game.Client.Systems.Local.Transform.Set;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Entity.Api;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

public struct SetEntityPositionEvent : INotification
{
    public long ClientId { get; }
    public IVector3 Position { get; }

    public SetEntityPositionEvent(long clientId, IVector3 position)
    {
        ClientId = clientId;
        Position = position;
    }
}

public interface SetEntityPositionEventObserver : ArgumentObserver<SetEntityPositionEvent> { }

public class SetEntityPositionEventHandler : INotificationHandler<SetEntityPositionEvent>
{
    private readonly ObserverState _observer;

    public SetEntityPositionEventHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(SetEntityPositionEvent notification, CancellationToken cancellationToken) =>
        _observer.Trigger<SetEntityPositionEventObserver, SetEntityPositionEvent>(
            notification,
            cancellationToken
        );
}
