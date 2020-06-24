namespace EventHorizon.Game.Client.Engine.Rendering.Model
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Exceptions;
    using EventHorizon.Game.Client.Engine.Rendering.Api;

    public class StandardRenderingScene
        : IRenderingScene
    {
        private ISceneImplementation _scene;

        private readonly IRenderingEngine _renderingEngine;

        public int Priority => -800;

        public StandardRenderingScene(
            IRenderingEngine renderingEngine
        )
        {
            _renderingEngine = renderingEngine;
        }

        public T GetScene<T>() where T : class, ISceneImplementation
        {
            return _scene as T;
        }

        public async Task Initialize()
        {
            var engine = _renderingEngine.GetEngine<StandardEngineImplementation>();
            if (engine.IsNull())
            {
                throw new GameRuntimeException(
                    "invalid_engine_implementation",
                    "Rendering Engine was null."
                );
            }

            _scene = await StandardSceneImplementation.Create(
                engine.Engine
            );
        }

        public Task Dispose()
        {
            if(!_scene.IsNull())
            {
                _scene.Dispose();
            }

            return Task.CompletedTask;
        }

        public string RegisterAfterRender(Func<Task> action)
        {
            return _scene.RegisterAfterRender(action);
        }

        public string RegisterBeforeRender(Func<Task> action)
        {
            return _scene.RegisterBeforeRender(action);
        }

        public void Render()
        {
            _scene.Render();
        }
    }
}
