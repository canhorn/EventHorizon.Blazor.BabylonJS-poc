namespace EventHorizon.Game.Client.Systems.Player.Modules.PlayerInteraction.WithIn;

using System;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using EventHorizon.Observer.Model;
using MediatR;

public struct EntityLeftInteractionDistanceEvent : INotification
{
    public IObjectEntity Entity { get; }

    public EntityLeftInteractionDistanceEvent(IObjectEntity entity)
    {
        Entity = entity;
    }
}

public interface EntityLeftInteractionDistanceEventObserver
    : ArgumentObserver<EntityLeftInteractionDistanceEvent> { }
