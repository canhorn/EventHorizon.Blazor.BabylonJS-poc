namespace EventHorizon.Game.Client.Engine.Rendering.Model
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Blazor.Interop;
    using EventHorizon.Game.Client.Core.Exceptions;
    using EventHorizon.Game.Client.Engine.Rendering.Api;

    public class BabylonJSRenderingScene
        : IRenderingScene
    {
        private BabylonJSSceneImplementation? _scene;

        private readonly IRenderingEngine _renderingEngine;

        public int Priority => 80_000;

        public BabylonJSRenderingScene(
            IRenderingEngine renderingEngine
        )
        {
            _renderingEngine = renderingEngine;
        }

        public T GetScene<T>() where T : class, ISceneImplementation
        {
            if (_scene is T typedScene)
            {
                return typedScene;
            }
            throw new GameRuntimeException(
                "scene_is_null",
                "The Scene was null."
            );
        }

        public Task Initialize()
        {
            var engine = _renderingEngine.GetEngine<BabylonJSEngineImplementation>();
            if (engine.IsNull())
            {
                throw new GameRuntimeException(
                    "invalid_engine_implementation",
                    "Rendering Engine was null."
                );
            }

            _scene = new BabylonJSSceneImplementation(
                engine.Engine
            );
            //EventHorizonBlazorInterop.Func<CachedEntity>(
            //    new object[]
            //    {
            //        new string[] { this._scene.Scene.___guid, "debugLayer", "show" },
            //        new
            //        {
            //            overlay = false
            //        }
            //    }
            //);

            return Task.CompletedTask;
        }

        public Task Dispose()
        {
            _scene?.Dispose();

            return Task.CompletedTask;
        }

        public string RegisterAfterRender(Func<Task> action)
        {
#if DEBUG
            if (_scene == null)
            {
                throw new GameRuntimeException(
                    "scene_is_null",
                    "The Scene was null."
                );
            }
#endif
            return _scene.RegisterAfterRender(action);
        }

        public string RegisterBeforeRender(Func<Task> action)
        {
#if DEBUG
            if (_scene == null)
            {
                throw new GameRuntimeException(
                    "scene_is_null",
                    "The Scene was null."
                );
            }
#endif
            return _scene.RegisterBeforeRender(action);
        }

        public void Render()
        {
#if DEBUG
            if (_scene == null)
            {
                throw new GameRuntimeException(
                    "scene_is_null",
                    "The Scene was null."
                );
            }
#endif
            _scene.Render();
        }
    }
}
