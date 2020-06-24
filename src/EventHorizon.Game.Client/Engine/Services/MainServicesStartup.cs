namespace EventHorizon.Game.Client.Engine.Services
{
    using EventHorizon.Game.Client.Engine.Services.Api;
    using EventHorizon.Game.Client.Engine.Services.Model;
    using Microsoft.Extensions.DependencyInjection;

    public static class MainServicesStartup
    {
        public static IServiceCollection AddEngineMainServices(
            this IServiceCollection services
        ) => services
            .AddSingleton<IDisposeServices, StandardDisposeServices>()
            .AddSingleton<IInitializeServices, StandardInitializeServices>()
            .AddSingleton<IGameService, StandardGameService>()
        ;
    }
}
