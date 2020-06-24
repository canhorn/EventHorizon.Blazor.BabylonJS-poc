namespace EventHorizon.Game.Client.Engine
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Monitoring.Api;
    using EventHorizon.Game.Client.Engine.Canvas.Api;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Api;
    using EventHorizon.Game.Client.Engine.Rendering.Api;
    using EventHorizon.Game.Client.Engine.Services.Api;
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
        private readonly IPlatformMonitor _platformMonitor;
        private readonly IGameService _gameService;

        private readonly IInitializeServices _initializeServices;

        //private readonly ICanvas _canvas;
        private readonly IRenderingEngine _renderingEngine;
        private readonly IRenderingScene _renderingScene;
        //private readonly IRenderingGui _renderingGui;

        private readonly IRegisterInitializable _registerInitializable;
        private readonly IRegisterDisposable _registerDisposable;
        private readonly IRegisterUpdatable _registerUpdatable;
        private readonly IBeforeRendering _beforeRendering;


        public Engine(
            ILogger<Engine> logger,
            IPlatformMonitor platformMonitor,
            IGameService gameService,

            IInitializeServices initializeServices,

            ICanvas canvas,
            IRenderingEngine renderingEngine,
            IRenderingScene renderingScene,
            IRenderingGui renderingGui,

            IRegisterInitializable registerInitializable,
            IRegisterDisposable registerDisposable,
            IRegisterUpdatable registerUpdatable,
            IBeforeRendering beforeRendering
        )
        {
            _logger = logger;
            _platformMonitor = platformMonitor;
            _gameService = gameService;

            _initializeServices = initializeServices;

            //_canvas = canvas;
            _renderingEngine = renderingEngine;
            _renderingScene = renderingScene;
            //_renderingGui = renderingGui;
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

            // Engine System Services
            //setupEngineSystemServices();

            // Particle Services
            //setupParticleServices();

            // Input services
            //setupInputServices();

            //if (debugEnabled())
            //{
            //    setupEngineDebugging();
            //}

            return Task.CompletedTask;
        }

        public Task Start()
        {
            _logger.LogDebug("Start");
            _platformMonitor.TrackEvent("Game:Start");
            _renderingScene.RegisterAfterRender(
                async () =>
                {
                    await _registerUpdatable.Run();
                }
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

            // Core 
            //await _canvas.Initialize();
            //await _renderingEngine.Initialize();
            //await _renderingScene.Initialize();
            //await _renderingGui.Initialize();

            // Systems (Engine.System namespace)
            // Loading (Engine.Loading namespace)
            // Particle (Engine.Particle namespace)

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

            await _registerDisposable.Run();
            _disposed = true;
            _platformMonitor.TrackEvent("Game:End");
        }
    }
}
