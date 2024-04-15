namespace EventHorizon.Game.Client.Systems.ServerModule.Api;

using System.Collections.Generic;
using EventHorizon.Game.Client.Systems.EntityModule.Api;

public interface ServerModuleState
{
    IEnumerable<IEntityLifeCycleModule> All();
    Option<IEntityLifeCycleModule> Get(string name);
    Option<IEntityLifeCycleModule> Remove(string name);
    Option<IEntityLifeCycleModule> Set(IEntityLifeCycleModule entityModule);
    void Clear();
}
