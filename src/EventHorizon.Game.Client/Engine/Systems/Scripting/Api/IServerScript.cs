using System.Collections.Generic;

namespace EventHorizon.Game.Client.Engine.Systems.Scripting.Api
{
    public interface IServerScript
    {
        bool IsRunnable { get; }
        T Run<T>(IDictionary<string, object> data);
    }
}