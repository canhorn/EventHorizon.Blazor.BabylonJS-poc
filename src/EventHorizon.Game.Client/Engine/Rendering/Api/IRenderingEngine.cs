namespace EventHorizon.Game.Client.Engine.Rendering.Api;

using System;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Lifecycle.Api;

public interface IRenderingEngine : IServiceEntity
{
    IEngineImplementation GetEngine();
    T GetEngine<T>()
        where T : class, IEngineImplementation;
    string RunRenderLoop(Func<Task> p);
}
