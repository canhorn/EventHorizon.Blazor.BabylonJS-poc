namespace EventHorizon.Game.Client.Engine.Rendering.Api
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Lifecycle.Api;
    using EventHorizon.Game.Client.Engine.Rendering.Api.Camera;

    public interface IRenderingScene : IServiceEntity
    {
        ICamera? ActiveCamera { get; }
        T GetScene<T>() where T : class, ISceneImplementation;
        string RegisterAfterRender(Func<Task> action);
        string RegisterBeforeRender(Func<Task> action);
        void Render();
    }
}
