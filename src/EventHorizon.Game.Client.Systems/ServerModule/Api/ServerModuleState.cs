namespace EventHorizon.Game.Client.Systems.ServerModule.Api
{
    using System.Collections.Generic;

    public interface ServerModuleState
    {
        IEnumerable<IServerModule> All();
        Option<IServerModule> Get(
            string name
        );
        Option<IServerModule> Remove(
            string name
        );
        Option<IServerModule> Set(
            IServerModule serverModule
        );
        void Clear();
    }
}