namespace EventHorizon.Game.Client.Systems.EntityModule.Api;

using System;
using System.Collections.Generic;

public interface EntityPlayerScriptModuleState
{
    IEnumerable<EntityModuleScripts> All();
    void Set(EntityModuleScripts baseModule);
    void Reset();
}
