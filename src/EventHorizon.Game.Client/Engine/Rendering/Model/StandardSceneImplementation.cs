namespace EventHorizon.Game.Client.Engine.Rendering.Model
{
    using System;
    using System.Threading.Tasks;
    using BabylonJS;
    using EventHorizon.Game.Client.Engine.Rendering.Api;

    public class StandardSceneImplementation
        : ISceneImplementation
    {
        public static async Task<StandardSceneImplementation> Create(
            Engine engine
        )
        {
            return new StandardSceneImplementation(
                await Scene.Create(
                    engine
                )
            );
        }

        public Scene Scene { get; }

        private StandardSceneImplementation(
            Scene scene
        )
        {
            Scene = scene;
        }

        public void Dispose()
        {
            Scene.Dispose();
        }

        public string RegisterBeforeRender(
            Func<Task> beforeRenderAction
        ) => Scene.RegisterBeforeRender(beforeRenderAction);

        public string RegisterAfterRender(
            Func<Task> afterRenderAction
        ) => Scene.RegisterAfterRender(afterRenderAction);

        public void Render()
        {
            Scene.Render();
        }
    }
}
