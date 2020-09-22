namespace EventHorizon.Game.Client.Engine
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Monitoring.Api;
    using EventHorizon.Game.Client.Engine.Lifecycle.Dispose;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Api;
    using EventHorizon.Game.Client.Engine.Rendering.Api;
    using EventHorizon.Game.Client.Engine.Services.Api;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public interface IEngine
    {
        Task Start();
        Task Dispose();
        Task Setup();
        Task PreInitialize();
        Task PostInitialize();
    }


    public class Engine : IEngine
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        private readonly IPlatformMonitor _platformMonitor;
        private readonly IGameService _gameService;

        private readonly IInitializeServices _initializeServices;
        private readonly IDisposeServices _disposeServices;

        private readonly IRenderingEngine _renderingEngine;
        private readonly IRenderingScene _renderingScene;

        private readonly IRegisterInitializable _registerInitializable;
        private readonly IRegisterDisposable _registerDisposable;
        private readonly IRegisterUpdatable _registerUpdatable;
        private readonly IBeforeRendering _beforeRendering;


        public Engine(
            ILogger<Engine> logger,
            IMediator mediator,
            IPlatformMonitor platformMonitor,
            IGameService gameService,

            IInitializeServices initializeServices,
            IDisposeServices disposeServices,

            IRenderingEngine renderingEngine,
            IRenderingScene renderingScene,

            IRegisterInitializable registerInitializable,
            IRegisterDisposable registerDisposable,
            IRegisterUpdatable registerUpdatable,
            IBeforeRendering beforeRendering
        )
        {
            _logger = logger;
            _mediator = mediator;
            _platformMonitor = platformMonitor;
            _gameService = gameService;

            _initializeServices = initializeServices;
            _disposeServices = disposeServices;

            _renderingEngine = renderingEngine;
            _renderingScene = renderingScene;
            _beforeRendering = beforeRendering;

            _registerInitializable = registerInitializable;
            _registerDisposable = registerDisposable;
            _registerUpdatable = registerUpdatable;

            _logger.LogInformation("Starting Engine");
            _platformMonitor.TrackEvent("Game:Starting");
        }

        private bool _disposed;

        public Task Setup()
        {
            _disposed = false;
            _logger.LogDebug("Setup");
            _platformMonitor.TrackEvent("Game:Setup");


            return Task.CompletedTask;
        }

        public Task Start()
        {
            _logger.LogDebug("Start");
            _platformMonitor.TrackEvent("Game:Start");
            _renderingScene.RegisterBeforeRender(
                () => _registerUpdatable.Run()
            );
            _renderingEngine.RunRenderLoop(
                () =>
                {
                    _renderingScene.Render();

                    return Task.CompletedTask;
                }
            );

            return Task.CompletedTask;
        }

        public async Task PreInitialize()
        {
            _logger.LogDebug("PreInitialize");
            _platformMonitor.TrackEvent("Game:PreInitialize:Start");

            // Startup all Standard Registered IServiceEntities
            await _initializeServices.InitializeServices();

            _platformMonitor.TrackEvent("Game:PreInitialize:End");
        }

        public async Task PostInitialize()
        {
            _logger.LogDebug("PostInitialize");
            _platformMonitor.TrackEvent("Game:PostInitialize");
            await _registerInitializable.Run();
            _beforeRendering.Register(async () =>
            {
                await _gameService.Get().Update();
            });
        }

        public async Task Dispose()
        {
            _logger.LogDebug("Dispose");
            if (_disposed)
            {
                return;
            }

            // Dispose of all Registered IServiceEntities
            await _mediator.Publish(
                new DisposeOfEngineEvent()
            );
            await _registerDisposable.Run();
            await _disposeServices.DisposeServices();

            _disposed = true;
            _platformMonitor.TrackEvent("Game:End");
        }
    }
}
