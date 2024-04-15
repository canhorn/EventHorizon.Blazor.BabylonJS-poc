namespace EventHorizon.Game.Client.Systems.EntityModule.State;

using System;
using System.Collections.Generic;
using EventHorizon.Game.Client.Systems.EntityModule.Api;

public class StandardEntityBaseScriptModuleState : EntityBaseScriptModuleState
{
    private readonly IDictionary<string, EntityModuleScripts> _map =
        new Dictionary<string, EntityModuleScripts>();

    public IEnumerable<EntityModuleScripts> All()
    {
        return _map.Values;
    }

    public void Reset()
    {
        _map.Clear();
    }

    public void Set(EntityModuleScripts baseModule)
    {
        _map[baseModule.Name] = baseModule;
    }
}
