namespace EventHorizon.Game.Client.Systems.ServerModule.Api
{
    using System.Collections.Generic;

    public interface ServerModuleScriptsState
    {
        IEnumerable<IServerModuleScripts> All();
        void Clear();
        void Set(
            IServerModuleScripts serverModuleScripts
        );
    }
}
