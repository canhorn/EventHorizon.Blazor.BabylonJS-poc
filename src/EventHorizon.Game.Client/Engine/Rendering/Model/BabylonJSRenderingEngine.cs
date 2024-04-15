namespace EventHorizon.Game.Client.Engine.Rendering.Model;

using System;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Exceptions;
using EventHorizon.Game.Client.Engine.Canvas.Api;
using EventHorizon.Game.Client.Engine.Rendering.Api;

public class BabylonJSRenderingEngine : IRenderingEngine
{
    private IEngineImplementation? _engine;

    private readonly ICanvas _canvas;

    public int Priority => 90_000;

    public BabylonJSRenderingEngine(ICanvas canvas)
    {
        _canvas = canvas;
    }

    public IEngineImplementation GetEngine()
    {
        return GetEngine<IEngineImplementation>();
    }

    public T GetEngine<T>()
        where T : class, IEngineImplementation
    {
        if (_engine is T typedEngine)
        {
            return typedEngine;
        }
        throw new GameRuntimeException(
            "engine_is_null",
            "The Engine has not been set, is currently null."
        );
    }

    public Task Initialize()
    {
        var canvas = _canvas.GetDrawingCanvas<Html.Interop.Canvas>();
        if (canvas.IsNull())
        {
            throw new GameRuntimeException("invalid_drawing_canvas", "Drawing Canvas was null.");
        }
        _engine = new BabylonJSEngineImplementation(canvas, true, true);
        // TODO: Implement Resize Event Listener on _engineImplementation
        // TODO: Implement _engineImplementation.Resize
        return Task.CompletedTask;
    }

    public Task Dispose()
    {
        _engine?.Dispose();

        return Task.CompletedTask;
    }

    public string RunRenderLoop(Func<Task> action)
    {
#if DEBUG
        if (_engine == null)
        {
            throw new GameRuntimeException(
                "engine_is_null",
                "The Engine has not been set, is currently null."
            );
        }
#endif
        return _engine.RunRenderLoop(action);
    }
}
