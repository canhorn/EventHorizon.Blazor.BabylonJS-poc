namespace EventHorizon.Game.Client;

using EventHorizon.Game.Client.Core;
using EventHorizon.Game.Client.Engine;
using EventHorizon.Game.Client.Engine.Canvas.Api;
using EventHorizon.Game.Client.Engine.Canvas.Model;
using EventHorizon.Game.Client.Engine.Core.Api;
using EventHorizon.Game.Client.Engine.Core.Model;
using EventHorizon.Game.Client.Engine.Entity;
using EventHorizon.Game.Client.Engine.Entity.Tracking;
using EventHorizon.Game.Client.Engine.Gui;
using EventHorizon.Game.Client.Engine.Input;
using EventHorizon.Game.Client.Engine.Lifecycle;
using EventHorizon.Game.Client.Engine.Particle;
using EventHorizon.Game.Client.Engine.Rendering;
using EventHorizon.Game.Client.Engine.Services;
using EventHorizon.Game.Client.Engine.Settings.Api;
using EventHorizon.Game.Client.Engine.Settings.Model;
using EventHorizon.Game.Client.Engine.Systems;
using EventHorizon.Game.Client.Engine.Window.Api;
using EventHorizon.Game.Client.Engine.Window.Model;
using Microsoft.Extensions.DependencyInjection;

public static class ClientExtensions
{
    public static IServiceCollection AddGameClient(this IServiceCollection services) =>
        services
            .AddCoreServices()
            .AddEngineSystemServices()
            .AddEngineRenderingServices()
            .AddEngineEntityServices()
            .AddEngineGuiServices()
            .AddEngineParticleServices()
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
            .AddSingleton<ISystemWindow, BrowserSystemWindow>();
}
