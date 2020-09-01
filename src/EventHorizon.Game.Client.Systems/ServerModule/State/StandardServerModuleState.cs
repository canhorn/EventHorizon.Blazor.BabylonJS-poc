namespace EventHorizon.Game.Client.Systems.ServerModule.State
{
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Systems.ServerModule.Api;

    public class StandardServerModuleState
        : ServerModuleState
    {
        private readonly IDictionary<string, IServerModule> _serverModules = new Dictionary<string, IServerModule>();

        public IEnumerable<IServerModule> All()
        {
            return _serverModules.Values;
        }

        public void Clear()
        {
            _serverModules.Clear();
        }

        public Option<IServerModule> Get(
            string name
        )
        {
            // Get the current from Dictionary
            if (_serverModules.TryGetValue(
                name,
                out var currentServerModule
            ))
            {
                return currentServerModule.ToOption();
            }
            return new Option<IServerModule>(
                null
            );
        }

        public Option<IServerModule> Set(
            IServerModule serverModule
        )
        {
            // Check/Get the current from Dictionary
            if (_serverModules.TryGetValue(
                serverModule.Name,
                out var currentServerModule
            ))
            {
                return currentServerModule.ToOption();
            }

            // Set in dictionary
            _serverModules[serverModule.Name] = serverModule;

            return new Option<IServerModule>(
                null
            );
        }

        public Option<IServerModule> Remove(
            string name
        )
        {
            // Check/Get the current from Dictionary
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

            return new Option<IServerModule>(
                null
            );
        }
    }
}
