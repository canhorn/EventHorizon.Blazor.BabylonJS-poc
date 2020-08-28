namespace EventHorizon.Game.Client.Systems.ClientScripts.Api
{
    using System;
    using System.Reflection;
    using EventHorizon.Game.Client.Engine.Model.Scripting.Api;

    public interface ClientScriptsState
    {
        string Hash { get; }
        void SetScriptAssembly(
            string hash,
            Assembly scriptAssembly
        );
        IClientScript GetScript(
            string id
        );
    }
}
