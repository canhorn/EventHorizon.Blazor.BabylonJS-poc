namespace EventHorizon.Game.Client
{
    using EventHorizon.Game.Client.Core;
    using EventHorizon.Game.Client.Engine;
    using EventHorizon.Game.Client.Engine.Canvas.Api;
    using EventHorizon.Game.Client.Engine.Canvas.Model;
    using EventHorizon.Game.Client.Engine.Core.Api;
    using EventHorizon.Game.Client.Engine.Core.Model;
    using EventHorizon.Game.Client.Engine.Entity;
    using EventHorizon.Game.Client.Engine.Entity.Tracking;
    using EventHorizon.Game.Client.Engine.Input;
    using EventHorizon.Game.Client.Engine.Lifecycle;
    using EventHorizon.Game.Client.Engine.Rendering;
    using EventHorizon.Game.Client.Engine.Services;
    using EventHorizon.Game.Client.Engine.Settings.Api;
    using EventHorizon.Game.Client.Engine.Settings.Model;
    using EventHorizon.Game.Client.Engine.Systems;
    using Microsoft.Extensions.DependencyInjection;

    public static class ClientExtensions
    {
        public static IServiceCollection AddGameClient(
            this IServiceCollection services
        ) => services
            .AddCoreServices()
            .AddEngineSystemServices()
            .AddEngineRenderingServices()
            .AddEngineEntityServices()
            .AddEngineLifecycleServices()
            .AddEngineMainServices()
            .AddEngineInputServices()
            .AddEngineEntityTrackingServices()
            // TODO: Register Base Services
            .AddSingleton<IIndexPool, IndexPoolBase>()
            .AddSingleton<IGameSettings, GameSettingsBase>()
            .AddServiceEntity<ICanvas, BabylonJSCanvas>()
            .AddTransient<IEngine, Engine.Engine>()
            .AddTransient<IStartup, Startup>()
        ;
    }
}
