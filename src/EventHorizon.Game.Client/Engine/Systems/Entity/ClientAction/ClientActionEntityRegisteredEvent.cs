namespace EventHorizon.Game.Client.Engine.Systems.Entity.ClientAction
{
    using System;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes;
    using MediatR;

    [ClientAction("EntityUnregistered")]
    public class ClientActionEntityUnregisteredEvent
        : INotification,
        IClientAction
    {
        public long EntityId { get; }

        public ClientActionEntityUnregisteredEvent(
            IClientActionDataResolver resolver
        )
        {
            EntityId = resolver.Resolve<long>("entityId");
        }
    }
}
