namespace EventHorizon.Game.Client.Systems.Map
{
    using EventHorizon.Game.Client.Systems.Map.Api;
    using EventHorizon.Game.Client.Systems.Map.State;
    using Microsoft.Extensions.DependencyInjection;

    public static class MapSystemStartup
    {
        public static IServiceCollection AddMapSystemServices(
            this IServiceCollection services
        ) => services
            .AddSingleton<IMapState, StandardMapMeshState>()
        ;
    }
}
