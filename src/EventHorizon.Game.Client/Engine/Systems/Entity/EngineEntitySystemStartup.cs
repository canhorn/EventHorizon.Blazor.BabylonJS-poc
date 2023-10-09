namespace EventHorizon.Game.Client.Engine.Systems.Entity;

using EventHorizon.Game.Client.Core.Mapper.Api;
using EventHorizon.Game.Client.Core.Mapper.Model;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using EventHorizon.Game.Client.Engine.Systems.Entity.Model;
using EventHorizon.Game.Client.Engine.Systems.Entity.State;

using Microsoft.Extensions.DependencyInjection;

public static class EngineEntitySystemStartup
{
    public static IServiceCollection AddEngineEntitySystemServices(
        this IServiceCollection services
    ) =>
        services
            .AddSingleton<IEntityDetailsState, StandardEntityDetailsState>()
            .AddSingleton<
                IMapper<IObjectEntityDetails>,
                StandardMapper<IObjectEntityDetails, ObjectEntityDetailsModel>
            >()
            .AddSingleton<
                IMapper<ObjectEntityConfiguration>,
                StandardMapper<
                    ObjectEntityConfiguration,
                    ObjectEntityConfigurationModel
                >
            >();
}
