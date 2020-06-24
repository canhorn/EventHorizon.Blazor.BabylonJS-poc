using System.Collections;
using System.Collections.Generic;
using EventHorizon.Game.Client.Engine.Systems.Scripting.Api;

namespace EventHorizon.Game.Client.Engine.Systems.ServerModule.Add
{
    public interface IServerModuleState
    {
        IEnumerable<IServerModule> All();
        void Set(IServerModule serverModule);
        void Clear();
    }
}