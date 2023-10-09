namespace EventHorizon.Game.Editor.Client.Zone.Agent.Selected;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;

using MediatR;

public struct AgentEntitySelectedEvent : INotification
{
    public IObjectEntityDetails Entity { get; }

    public AgentEntitySelectedEvent(IObjectEntityDetails entity)
    {
        Entity = entity;
    }
}

public interface AgentEntitySelectedEventObserver
    : ArgumentObserver<AgentEntitySelectedEvent> { }

public class AgentEntitySelectedEventObserverHandler
    : INotificationHandler<AgentEntitySelectedEvent>
{
    private readonly ObserverState _observer;

    public AgentEntitySelectedEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        AgentEntitySelectedEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            AgentEntitySelectedEventObserver,
            AgentEntitySelectedEvent
        >(notification, cancellationToken);
}
