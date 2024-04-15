namespace EventHorizon.Game.Client.Engine.Entity.Model;

using System.Collections.Generic;
using EventHorizon.Game.Client.Engine.Entity.Api;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;

public class ClientEntityBase : IClientEntity
{
    public long ClientId { get; }

    public ClientEntityBase(long clientId)
    {
        ClientId = clientId;
    }
}
