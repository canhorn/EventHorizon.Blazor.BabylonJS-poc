namespace EventHorizon.Game.Client
{
    using EventHorizon.Game.Client.Core;
    using EventHorizon.Game.Client.Engine;
    using EventHorizon.Game.Client.Engine.Canvas.Api;
    using EventHorizon.Game.Client.Engine.Canvas.Model;
    using EventHorizon.Game.Client.Engine.Lifecycle;
    using EventHorizon.Game.Client.Engine.Rendering;
    using EventHorizon.Game.Client.Engine.Services;
    using EventHorizon.Game.Client.Engine.Settings.Api;
    using EventHorizon.Game.Client.Engine.Settings.Model;
    using EventHorizon.Game.Client.Engine.Systems;
    using EventHorizon.Observer.Admin.State;
    using EventHorizon.Observer.State;
    using Microsoft.Extensions.DependencyInjection;

    public static class ClientExtensions
    {
        public static IServiceCollection AddGameClient(
            this IServiceCollection services
        ) => services
            // TODO: [Research] : Is Singleton the correct lifetime here?
            .AddCoreServices()
            .AddEngineSystemServices()
            .AddEngineRenderingServices()
            .AddEngineLifecycleServices()
            .AddEngineMainServices()
            // TODO: Register Base Services
            .AddSingleton<IGameSettings, GameSettingsBase>()
            .AddServiceEntity<ICanvas, StandardCanvas>()
            .AddTransient<IEngine, Engine.Engine>()
            .AddTransient<IStartup, Startup>()
        ;
    }
}
