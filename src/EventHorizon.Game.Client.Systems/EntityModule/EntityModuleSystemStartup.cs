namespace EventHorizon.Game.Client.Systems;

using EventHorizon.Game.Client.Systems.EntityModule.Api;
using EventHorizon.Game.Client.Systems.EntityModule.State;

using Microsoft.Extensions.DependencyInjection;

public static class EntityModuleSystemStartup
{
    public static IServiceCollection AddEntityModuleSystemServices(
        this IServiceCollection services
    ) =>
        services
            .AddSingleton<
                EntityBaseScriptModuleState,
                StandardEntityBaseScriptModuleState
            >()
            .AddSingleton<
                EntityPlayerScriptModuleState,
                StandardEntityPlayerScriptModuleState
            >();
}
