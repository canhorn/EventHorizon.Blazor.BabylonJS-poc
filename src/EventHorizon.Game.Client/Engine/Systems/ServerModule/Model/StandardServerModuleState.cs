using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventHorizon.Game.Client.Engine.Systems.Scripting.Api;
using EventHorizon.Game.Client.Engine.Systems.ServerModule.Add;

namespace EventHorizon.Game.Client.Engine.Systems.ServerModule.Model
{
    public class StandardServerModuleState
        : IServerModuleState
    {
        private readonly ConcurrentBag<IServerModule> _serverModules = new ConcurrentBag<IServerModule>();

        public IEnumerable<IServerModule> All()
        {
            return _serverModules;
        }

        public void Clear()
        {
            _serverModules.Clear();
        }

        public void Set(
            IServerModule serverModule
        )
        {
            var currentServerModule = _serverModules.FirstOrDefault(
                a => a.Name == serverModule.Name
            );
            if (currentServerModule != null)
            {
                currentServerModule.Dispose();
                _serverModules.RemoveItem(
                    currentServerModule
                );
            }
            _serverModules.Add(
                serverModule
            );
        }
    }
}
