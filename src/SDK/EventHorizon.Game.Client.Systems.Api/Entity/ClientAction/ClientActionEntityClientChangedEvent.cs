namespace EventHorizon.Game.Client.Systems.Entity.ClientAction
{
    using System;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Observer.Model;

    [ClientAction("EntityClientChanged")]
    public struct ClientActionEntityClientChangedEvent
        : IClientAction
    {
        public IObjectEntityDetails Details { get; }

        public ClientActionEntityClientChangedEvent(
            IClientActionDataResolver resolver
        )
        {
            Details = resolver.Resolve<IObjectEntityDetails>("details");
        }
    }

    public interface ClientActionEntityClientChangedEventObserver
        : ArgumentObserver<ClientActionEntityClientChangedEvent>
    {
    }
}
