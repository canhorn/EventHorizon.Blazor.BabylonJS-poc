namespace EventHorizon.Game.Client.Systems.ServerModule.State;

using System.Collections.Generic;

using EventHorizon.Game.Client.Systems.EntityModule.Api;
using EventHorizon.Game.Client.Systems.ServerModule.Api;

public class StandardServerModuleState : ServerModuleState
{
    private readonly IDictionary<
        string,
        IEntityLifeCycleModule
    > _serverModules = new Dictionary<string, IEntityLifeCycleModule>();

    public IEnumerable<IEntityLifeCycleModule> All()
    {
        return _serverModules.Values;
    }

    public void Clear()
    {
        _serverModules.Clear();
    }

    public Option<IEntityLifeCycleModule> Get(string name)
    {
        if (_serverModules.TryGetValue(name, out var currentServerModule))
        {
            return currentServerModule.ToOption();
        }
        return new Option<IEntityLifeCycleModule>(null);
    }

    public Option<IEntityLifeCycleModule> Set(
        IEntityLifeCycleModule entityModule
    )
    {
        if (
            _serverModules.TryGetValue(
                entityModule.Name,
                out var currentServerModule
            )
        )
        {
            return currentServerModule.ToOption();
        }

        // Set in dictionary
        _serverModules[entityModule.Name] = entityModule;

        return new Option<IEntityLifeCycleModule>(null);
    }

    public Option<IEntityLifeCycleModule> Remove(string name)
    {
        if (_serverModules.TryGetValue(name, out var serverModule))
        {
            _serverModules.Remove(name);
            return serverModule.ToOption();
        }

        return new Option<IEntityLifeCycleModule>(null);
    }
}
