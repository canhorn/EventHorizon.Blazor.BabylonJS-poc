namespace EventHorizon.Game.Client.Systems.ServerModule.Actions
{
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Systems.ServerModule.Api;
    using MediatR;

    // TODO: [ClientAction] : Finish Implementation
    //[ClientAction("SERVER_MODULE_SYSTEM_RELOADED_CLIENT_ACTION_EVENT")]
    public struct ServerModuleSystemReloadedClientAction : INotification
    {
        public IList<IServerModuleScripts> ServerModuleScriptList { get; set; }
    }
}
