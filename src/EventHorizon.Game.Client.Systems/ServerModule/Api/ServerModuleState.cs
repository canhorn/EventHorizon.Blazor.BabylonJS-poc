namespace EventHorizon.Game.Client.Systems.ServerModule.Api
{
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Systems.EntityModule.Api;

    public interface ServerModuleState
    {
        IEnumerable<IEntityModule> All();
        Option<IEntityModule> Get(
            string name
        );
        Option<IEntityModule> Remove(
            string name
        );
        Option<IEntityModule> Set(
            IEntityModule entityModule
        );
        void Clear();
    }
}