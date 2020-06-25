namespace EventHorizon.Game.Client.Engine.Canvas.Model
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Exceptions;
    using EventHorizon.Game.Client.Engine.Canvas.Api;
    using EventHorizon.Game.Client.Engine.Canvas.Initialized;
    using EventHorizon.Game.Client.Engine.Canvas.Reset;
    using EventHorizon.Game.Client.Engine.Settings.Api;
    using EventHorizon.Observer.Register;
    using EventHorizon.Observer.Unregister;
    using MediatR;

    public class BabylonJSCanvas
        : ICanvas,
        CanvasResetObserver
    {
        private BabylonJS.Html.Canvas? _canvas;

        private readonly IMediator _mediator;
        private readonly IGameSettings _gameSettings;

        public int Priority => 100_000;

        public BabylonJSCanvas(
            IMediator mediator,
            IGameSettings gameSettings
        )
        {
            _mediator = mediator;
            _gameSettings = gameSettings;
        }

        public T GetDrawingCanvas<T>() where T : class
        {
            if(_canvas is T typedCanvas)
            {
                return typedCanvas;
            }
            throw new GameRuntimeException(
                "canvas_not_initialized",
                "Canvas is not Intialized"
            );
        }

        public async Task Initialize()
        {
            // Register Observer
            await _mediator.Send(
                new RegisterObserverCommand(this)
            );
            if(_gameSettings.CanvasTagId.IsNull())
            {
                throw new Exception();
            }
            _canvas = BabylonJS.Html.Canvas.Create(
                _gameSettings.CanvasTagId
            );
            await _mediator.Publish(
                new CanvasInitialized()
            );
        }

        public async Task Dispose()
        {
            await _mediator.Send(
                new UnregisterObserverCommand(this)
            );
            _canvas = null;
        }

        public async Task Handle(
            CanvasReset _
        )
        {
            await Dispose();
            await Initialize();
            await _mediator.Send(
                new CanvasResetFinished()
            );
        }
    }
}
