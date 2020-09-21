namespace EventHorizon.Game.Client
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Configuration;
    using EventHorizon.Game.Client.Engine;
    using EventHorizon.Game.Client.Engine.Services.Api;
    using EventHorizon.Game.Client.Engine.Systems.Player.Api;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public interface IStartup
    {
        void Setup(
            IGame game,
            string appendToTag,
            IPlayerDetails playerDetails,
            string accountLoginUrl,
            string coreServerUrl,
            string assetServerUrl,
            string applicationInsightsKey
        );
        Task Restart();
        Task Run();
        Task Stop();
    }

    public class Startup 
        : IStartup
    {
        private readonly ILogger _logger;
        private readonly IEngine _engine;
        private readonly IGameService _gameService;
        private readonly IServiceScope _serviceScope;

        public Startup(
            ILogger<Startup> logger,
            IEngine engine,
            IGameService gameService,
            IServiceScopeFactory scopeFactory
        )
        {
            _logger = logger;
            _engine = engine;
            _gameService = gameService;
            _serviceScope = scopeFactory.CreateScope();
            GameServiceProvider.SetServiceProvider(
                _serviceScope.ServiceProvider
            );
            GamePlatfrom.Setup();
        }

        public void Setup(
            IGame game,
            string appendToTag,
            IPlayerDetails playerDetails,
            string accountLoginUrl,
            string coreServerUrl,
            string assetServerUrl,
            string applicationInsightsKey
        )
        {
            _gameService.Set(
                game
            );
            Configuration.SetConfig(
                "APPEND_TO_TAG",
                appendToTag
            );
            Configuration.SetConfig(
                "PLAYER_DETAILS",
                playerDetails
            );
            Configuration.SetConfig(
                "USER_ACCESS_TOKEN",
                playerDetails.AccessToken
            );
            Configuration.SetConfig(
                "ACCOUNT_LOGIN_URL",
                accountLoginUrl
            );
            Configuration.SetConfig(
                "CORE_SERVER_URL",
                coreServerUrl
            );
            Configuration.SetConfig(
                "ASSET_SERVER",
                assetServerUrl
            );
            Configuration.SetConfig(
                "APPLICATION_INSIGHTS_INSTRUMENTATION_KEY",
                applicationInsightsKey
            );
        }

        public async Task Restart()
        {
            await Stop();
            await Run();
        }

        public async Task Run()
        {
            // TODO: [PerformanceTimer] : Implement performanceTimer
            //performanceTimer("[Startup]: Run");
            //this._engine = new Engine();
            //this._game = new this._gameType();

            await Setup();
            await DoInitialize();
            await DoStart();

            // TODO: [PerformanceTimer] : Implement performanceTimer
            //this._logger.LogInformation(
            //    "Run performance: ",
            //    performanceTimerEnd("[Startup]: Run")
            //);
        }

        public async Task Stop()
        {
            _logger.LogInformation("Stopping");
            await Dispose();
        }

        private async Task Dispose()
        {
            _logger.LogInformation("Disposing");
            await _gameService.Dispose();
            await _engine.Dispose();
            //cleanUpSystemServices();
        }

        private async Task Setup()
        {
            _logger.LogInformation("Setup");
            await _engine.Setup();
            await _gameService.Get().Setup();
        }

        private async Task DoInitialize()
        {
            _logger.LogInformation("Initializing");
            await _engine.PreInitialize();
            await _gameService.Get().Initialize();
            await _engine.PostInitialize();
        }

        private async Task DoStart()
        {
            _logger.LogInformation("Starting");
            await _engine.Start();
            await _gameService.Get().Start();
        }
    }
}
