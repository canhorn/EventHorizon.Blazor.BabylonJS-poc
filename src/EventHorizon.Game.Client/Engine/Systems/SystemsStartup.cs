namespace EventHorizon.Game.Client.Engine.Systems
{
    using EventHorizon.Game.Client.Engine.Systems.Camera;
    using EventHorizon.Game.Client.Engine.Systems.Entity;
    using Microsoft.Extensions.DependencyInjection;

    public static class SystemsStartup
    {
        public static IServiceCollection AddEngineSystemServices(
            this IServiceCollection services
        ) => services
            .AddEngineCameraSystemServices()
            .AddEngineEntitySystemServices()
        ;
    }
}
