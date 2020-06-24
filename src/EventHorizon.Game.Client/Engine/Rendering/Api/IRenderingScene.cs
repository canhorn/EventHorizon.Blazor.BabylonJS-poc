namespace EventHorizon.Game.Client.Engine.Rendering.Api
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Lifecycle.Api;

    public interface IRenderingScene : IServiceEntity
    {
        T GetScene<T>() where T : class, ISceneImplementation;
        string RegisterAfterRender(Func<Task> action);
        string RegisterBeforeRender(Func<Task> action);
        void Render();
    }
}
