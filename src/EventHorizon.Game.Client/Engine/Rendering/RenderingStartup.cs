namespace EventHorizon.Game.Client.Engine.Rendering;

using EventHorizon.Game.Client.Engine.Lifecycle.Api;
using EventHorizon.Game.Client.Engine.Rendering.Api;
using EventHorizon.Game.Client.Engine.Rendering.Model;

using Microsoft.Extensions.DependencyInjection;

public static class RenderingStartup
{
    public static IServiceCollection AddEngineRenderingServices(
        this IServiceCollection services
    ) =>
        services
            .AddServiceEntity<IRenderingEngine, BabylonJSRenderingEngine>()
            .AddServiceEntity<IRenderingScene, BabylonJSRenderingScene>()
            .AddServiceEntity<IRenderingGui, BabylonJSRenderingGui>()
            .AddSingleton<IBeforeRendering, BeforeRenderingBase>()
            .AddSingleton<IRenderingTime, RenderingTimeBase>();
}
