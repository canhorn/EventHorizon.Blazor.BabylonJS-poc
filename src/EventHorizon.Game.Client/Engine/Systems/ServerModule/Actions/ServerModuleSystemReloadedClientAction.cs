using System;
using System.Collections.Generic;
using System.Text;
using EventHorizon.Game.Client.Engine.Systems.ServerModule.Model;
using MediatR;

namespace EventHorizon.Game.Client.Engine.Systems.ServerModule.Actions
{
    // TODO: [ClientAction] : Finish Implementation
    //[ClientAction("SERVER_MODULE_SYSTEM_RELOADED_CLIENT_ACTION_EVENT")]
    public struct ServerModuleSystemReloadedClientAction : INotification
    {
        public static string ACTION_NAME = "SERVER_MODULE_SYSTEM_RELOADED_CLIENT_ACTION_EVENT";
        public IList<ServerModuleScripts> ServerModuleScriptList { get; set; }
    }
}
