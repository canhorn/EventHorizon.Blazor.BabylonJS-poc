namespace EventHorizon.Game.Client.Engine.Systems.Entity.ClientAction;

using System;

using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
using EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;

using MediatR;

[ClientAction("EntityRegistered")]
public class ClientActionEntityRegisteredEvent : INotification, IClientAction
{
    public IObjectEntityDetails Entity { get; }

    public ClientActionEntityRegisteredEvent(IClientActionDataResolver resolver)
    {
        Entity = resolver.Resolve<IObjectEntityDetails>("entity");
    }
}
