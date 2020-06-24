using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Exceptions;
using EventHorizon.Game.Client.Engine.Canvas.Api;
using EventHorizon.Game.Client.Engine.Canvas.Initialized;
using EventHorizon.Game.Client.Engine.Canvas.Reset;
using EventHorizon.Game.Client.Engine.Settings.Api;
using EventHorizon.Observer.Register;
using EventHorizon.Observer.Unregister;
using MediatR;

namespace EventHorizon.Game.Client.Engine.Canvas.Model
{
    public class StandardCanvas
        : ICanvas,
        CanvasResetObserver
    {
        private BabylonJS.Html.Canvas _canvas;

        private readonly IMediator _mediator;
        private readonly IGameSettings _gameSettings;

        public int Priority => -1000;

        public StandardCanvas(
            IMediator mediator,
            IGameSettings gameSettings
        )
        {
            _mediator = mediator;
            _gameSettings = gameSettings;
        }

        public T GetDrawingCanvas<T>() where T : class
        {
            if (_canvas == null)
            {
                throw new GameRuntimeException(
                    "canvas_not_initialized",
                    "Canvas is not Intialized"
                );
            }
            return _canvas as T;
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
            _canvas = await BabylonJS.Html.Canvas.Create(
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
