namespace EventHorizon.Game.Client.Systems.Local.Transform.Set;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Entity.Api;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

public struct SetEntityScalingEvent : INotification
{
    public long ClientId { get; }
    public IVector3 Scaling { get; }

    public SetEntityScalingEvent(long clientId, IVector3 scaling)
    {
        ClientId = clientId;
        Scaling = scaling;
    }
}

public interface SetEntityScalingEventObserver : ArgumentObserver<SetEntityScalingEvent> { }

public class SetEntityScalingEventHandler : INotificationHandler<SetEntityScalingEvent>
{
    private readonly ObserverState _observer;

    public SetEntityScalingEventHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(SetEntityScalingEvent notification, CancellationToken cancellationToken) =>
        _observer.Trigger<SetEntityScalingEventObserver, SetEntityScalingEvent>(
            notification,
            cancellationToken
        );
}
