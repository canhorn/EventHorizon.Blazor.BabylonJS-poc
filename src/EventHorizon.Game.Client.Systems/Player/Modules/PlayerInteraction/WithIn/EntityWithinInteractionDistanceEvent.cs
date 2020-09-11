namespace EventHorizon.Game.Client.Systems.Player.Modules.PlayerInteraction.WithIn
{
    using System;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Observer.Model;
    using MediatR;

    public struct EntityWithinInteractionDistanceEvent 
        : INotification
    {
        public IObjectEntity Entity { get; }
        public decimal DistanceToPlayer { get; }

        public EntityWithinInteractionDistanceEvent(
            IObjectEntity entity,
            decimal distanceToPlayer
        )
        {
            Entity = entity;
            DistanceToPlayer = distanceToPlayer;
        }
    }

    public interface EntityWithinInteractionDistanceEventObserver
        : ArgumentObserver<EntityWithinInteractionDistanceEvent>
    {
    }
}
