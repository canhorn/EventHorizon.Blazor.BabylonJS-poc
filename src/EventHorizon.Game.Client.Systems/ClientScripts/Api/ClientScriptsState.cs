namespace EventHorizon.Game.Client.Systems.ClientScripts.Api;

using System.Collections.Generic;
using System.Reflection;

using EventHorizon.Game.Client.Engine.Scripting.Api;

public interface ClientScriptsState
{
    string Hash { get; }
    IEnumerable<IStartupClientScript> StartupScripts { get; }
    void SetScriptAssembly(string hash, Assembly scriptAssembly);
    Option<IClientScript> GetScript(string id);
    void Reset();
}
