namespace EventHorizon.Game.Client.Systems.ServerModule.State
{
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Systems.EntityModule.Api;
    using EventHorizon.Game.Client.Systems.ServerModule.Api;

    public class StandardServerModuleState
        : ServerModuleState
    {
        private readonly IDictionary<string, IEntityModule> _serverModules = new Dictionary<string, IEntityModule>();

        public IEnumerable<IEntityModule> All()
        {
            return _serverModules.Values;
        }

        public void Clear()
        {
            _serverModules.Clear();
        }

        public Option<IEntityModule> Get(
            string name
        )
        {
            if (_serverModules.TryGetValue(
                name,
                out var currentServerModule
            ))
            {
                return currentServerModule.ToOption();
            }
            return new Option<IEntityModule>(
                null
            );
        }

        public Option<IEntityModule> Set(
            IEntityModule entityModule
        )
        {
            if (_serverModules.TryGetValue(
                entityModule.Name,
                out var currentServerModule
            ))
            {
                return currentServerModule.ToOption();
            }

            // Set in dictionary
            _serverModules[entityModule.Name] = entityModule;

            return new Option<IEntityModule>(
                null
            );
        }

        public Option<IEntityModule> Remove(
            string name
        )
        {
            if (_serverModules.TryGetValue(
                name,
                out var serverModule
            ))
            {
                _serverModules.Remove(
                    name
                );
                return serverModule.ToOption();
            }

            return new Option<IEntityModule>(
                null
            );
        }
    }
}
