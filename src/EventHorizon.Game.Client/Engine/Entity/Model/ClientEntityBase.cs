using System;
using System.Collections.Generic;
using System.Text;
using EventHorizon.Game.Client.Engine.Entity.Api;

namespace EventHorizon.Game.Client.Engine.Entity.Model
{
    public class ClientEntityBase : IClientEntity
    {
        public long ClientId { get; }

        public ClientEntityBase(
            long clientId
        )
        {
            ClientId = clientId;
        }
    }
}
