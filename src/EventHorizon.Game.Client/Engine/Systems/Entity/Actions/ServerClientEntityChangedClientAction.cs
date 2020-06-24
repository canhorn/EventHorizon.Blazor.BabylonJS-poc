using System;
using System.Collections.Generic;
using System.Text;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using MediatR;

namespace EventHorizon.Game.Client.Engine.Systems.Entity.Actions
{
    // TODO: [ClientAction] : Finish Implementation
    //[ClientAction("SERVER_CLIENT_ENTITY_CHANGED_CLIENT_ACTION_EVENT")]
    public struct ServerClientEntityChangedClientAction : INotification
    {
        public static string ACTION_NAME = "SERVER_CLIENT_ENTITY_CHANGED_CLIENT_ACTION_EVENT";
        public IObjectEntityDetails Details { get; set; }
    }
}
