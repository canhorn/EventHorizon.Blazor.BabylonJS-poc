namespace EventHorizon.Game.Client.Systems;

using EventHorizon.Game.Client.Core.Mapper.Api;
using EventHorizon.Game.Client.Core.Mapper.Model;
using EventHorizon.Game.Client.Systems.Map.Api;
using EventHorizon.Game.Client.Systems.Map.Model;
using EventHorizon.Game.Client.Systems.Map.State;
using Microsoft.Extensions.DependencyInjection;

public static class MapSystemStartup
{
    public static IServiceCollection AddMapSystemServices(this IServiceCollection services) =>
        services
            .AddSingleton<IMapState, StandardMapMeshState>()
            .AddSingleton<
                IMapper<IMapMeshDetails>,
                StandardMapper<IMapMeshDetails, MapMeshDetailsModel>
            >();
}
