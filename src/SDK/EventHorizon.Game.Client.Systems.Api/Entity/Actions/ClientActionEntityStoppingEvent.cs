namespace EventHorizon.Game.Client.Systems.Entity.Actions
{
    using System;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes;
    using EventHorizon.Observer.Model;
    using MediatR;

    [ClientAction("ClientEntityStopping")]
    public struct ClientActionEntityStoppingEvent
        : IClientAction
    {
        public long EntityId { get; }

        public ClientActionEntityStoppingEvent(
            IClientActionDataResolver resolver
        )
        {
            EntityId = resolver.Resolve<long>("entityId");
        }
    }

    public interface ClientActionEntityStoppingEventObserver
        : ArgumentObserver<ClientActionEntityStoppingEvent>
    {
    }
}
