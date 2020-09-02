namespace EventHorizon.Game.Client.Systems.Entity.Actions
{
    using System;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Observer.Model;
    using MediatR;

    [ClientAction("EntityClientChanged")]
    public struct ClientActionEntityClientChangedEvent
        : INotification,
        IClientAction
    {
        public IObjectEntityDetails Details { get; }

        public ClientActionEntityClientChangedEvent(
            IClientActionDataResolver resolver
        )
        {
            // TODO: Test This ( add in DetailsModule)
            Details = resolver.Resolve<IObjectEntityDetails>("details");
        }
    }

    public interface ClientActionEntityChangedEventObserver
        : ArgumentObserver<ClientActionEntityClientChangedEvent>
    {
    }
}
