namespace EventHorizon.Game.Client.Engine.Rendering.Model
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Canvas.Api;
    using EventHorizon.Game.Client.Engine.Rendering.Api;
    using EventHorizon.Game.Client.Core.Exceptions;
    using BabylonJS.Html;
    using System;

    public class StandardRenderingEngine
        : IRenderingEngine
    {
        private IEngineImplementation _engine;

        private readonly ICanvas _canvas;

        public int Priority => -900;

        public StandardRenderingEngine(
            ICanvas canvas
        )
        {
            _canvas = canvas;
        }

        public IEngineImplementation GetEngine()
        {
            return GetEngine<IEngineImplementation>();
        }

        public T GetEngine<T>() where T : class, IEngineImplementation
        {
            return _engine as T;
        }

        public async Task Initialize()
        {
            var canvas = _canvas.GetDrawingCanvas<Canvas>();
            if (canvas.IsNull())
            {
                throw new GameRuntimeException(
                    "invalid_drawing_canvas",
                    "Drawing Canvas was null."
                );
            }
            _engine = await StandardEngineImplementation.Create(
                canvas,
                true,
                true
            );
            // TODO: Implement Resize Event Listener on _engineImplementation
            // TODO: Implement _engineImplementation.Resize
        }

        public Task Dispose()
        {
            if (!_engine.IsNull())
            {
                _engine.Dispose();
            }

            return Task.CompletedTask;
        }

        public string RunRenderLoop(Func<Task> action)
        {
            return _engine.RunRenderLoop(action);
        }
    }
}
