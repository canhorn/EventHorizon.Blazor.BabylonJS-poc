namespace EventHorizon.Game.Client.Systems.ClientScripts.Api
{
    using System;
    using System.Reflection;
    using EventHorizon.Game.Client.Engine.Scripting.Api;

    public interface ClientScriptsState
    {
        string Hash { get; }
        void SetScriptAssembly(
            string hash,
            Assembly scriptAssembly
        );
        Option<IClientScript> GetScript(
            string id
        );
    }
}
