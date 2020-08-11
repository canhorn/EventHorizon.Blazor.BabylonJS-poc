namespace EventHorizon.Game.Client.Engine.Systems.Scripting.Actions
{
    using System;
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Engine.Systems.Scripting.Api;

    public class ClientScriptsSystemReloadedClientActionEvent
    {
        public IList<IClientScriptTemplate> ClientScriptList { get; }

        public ClientScriptsSystemReloadedClientActionEvent(
            IList<IClientScriptTemplate> clientScriptList
        )
        {
            ClientScriptList = clientScriptList;
        }
    }
}
