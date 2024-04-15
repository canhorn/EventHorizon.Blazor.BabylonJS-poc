namespace EventHorizon.Game.Client.Systems.Entity.Changed;

using System;
using EventHorizon.Observer.Model;
using MediatR;

public struct EntityChangedSuccessfullyEvent : INotification
{
    public long EntityId { get; }

    public EntityChangedSuccessfullyEvent(long entityId)
    {
        EntityId = entityId;
    }
}

public interface EntityChangedSuccessfullyEventObserver
    : ArgumentObserver<EntityChangedSuccessfullyEvent> { }
