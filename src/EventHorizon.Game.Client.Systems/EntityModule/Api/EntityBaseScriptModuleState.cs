namespace EventHorizon.Game.Client.Systems.EntityModule.Api
{
    using System;
    using System.Collections.Generic;

    public interface EntityBaseScriptModuleState
    {
        IEnumerable<EntityModuleScripts> All();
        void Set(
            EntityModuleScripts baseModule
        );
    }
}
