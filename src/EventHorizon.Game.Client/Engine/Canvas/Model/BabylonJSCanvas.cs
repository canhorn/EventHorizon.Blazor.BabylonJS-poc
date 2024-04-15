namespace EventHorizon.Game.Client.Engine.Canvas.Model;

using System;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Exceptions;
using EventHorizon.Game.Client.Engine.Canvas.Api;
using EventHorizon.Game.Client.Engine.Canvas.Initialized;
using EventHorizon.Game.Client.Engine.Canvas.Reset;
using EventHorizon.Game.Client.Engine.Settings.Api;
using MediatR;

public class BabylonJSCanvas : ICanvas, CanvasResetObserver
{
    private Html.Interop.Canvas? _canvas;

    private readonly IMediator _mediator;
    private readonly IGameSettings _gameSettings;

    public int Priority => 100_000;

    public BabylonJSCanvas(IMediator mediator, IGameSettings gameSettings)
    {
        _mediator = mediator;
        _gameSettings = gameSettings;
    }

    public T GetDrawingCanvas<T>()
        where T : class
    {
        if (_canvas is T typedCanvas)
        {
            return typedCanvas;
        }
        throw new GameRuntimeException("canvas_not_initialized", "Canvas is not Initialized");
    }

    public async Task Initialize()
    {
        // Register Observer
        GamePlatform.RegisterObserver(this);
        if (_gameSettings.CanvasTagId.IsNull())
        {
            throw new Exception();
        }
        _canvas = Html.Interop.Canvas.Create(_gameSettings.CanvasTagId);
        await _mediator.Publish(new CanvasInitialized());
    }

    public Task Dispose()
    {
        GamePlatform.UnRegisterObserver(this);
        _canvas = null;

        return Task.CompletedTask;
    }

    public async Task Handle(CanvasReset _)
    {
        await Dispose();
        await Initialize();
        await _mediator.Publish(new CanvasResetFinished());
    }
}
