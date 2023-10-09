namespace EventHorizon.Game.Client.Systems.Entity.ClientAction;

using System;

using EventHorizon.Game.Client.Engine.Entity.Api;
using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
using EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes;
using EventHorizon.Observer.Model;

[ClientAction("EntityClientMove")]
public class ClientActionEntityMoveEvent : IClientAction
{
    public long EntityId { get; }
    public IVector3 MoveTo { get; }

    public ClientActionEntityMoveEvent(IClientActionDataResolver resolver)
    {
        EntityId = resolver.Resolve<long>("entityId");
        MoveTo = resolver.Resolve<IVector3>("moveTo");
    }
}

public interface ClientActionEntityMoveEventObserver
    : ArgumentObserver<ClientActionEntityMoveEvent> { }
