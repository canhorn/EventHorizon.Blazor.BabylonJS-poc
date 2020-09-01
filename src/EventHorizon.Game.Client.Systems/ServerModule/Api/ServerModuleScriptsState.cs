namespace EventHorizon.Game.Client.Systems.ServerModule.Api
{
    using System.Collections.Generic;

    public interface ServerModuleScriptsState
    {
        IEnumerable<IServerModuleScripts> All();
        void Add(
            IServerModuleScripts serverModuleScripts
        );
    }
}
