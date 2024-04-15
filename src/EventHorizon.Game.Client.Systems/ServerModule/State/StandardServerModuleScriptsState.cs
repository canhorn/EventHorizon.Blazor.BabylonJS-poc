namespace EventHorizon.Game.Client.Systems.ServerModule.State;

using System.Collections.Generic;
using EventHorizon.Game.Client.Systems.ServerModule.Api;

public class StandardServerModuleScriptsState : ServerModuleScriptsState
{
    private readonly IDictionary<string, IServerModuleScripts> _serverModules =
        new Dictionary<string, IServerModuleScripts>();

    public IEnumerable<IServerModuleScripts> All()
    {
        return _serverModules.Values;
    }

    public void Clear()
    {
        _serverModules.Clear();
    }

    public void Set(IServerModuleScripts serverModuleScript)
    {
        _serverModules[serverModuleScript.Name] = serverModuleScript;
    }
}
