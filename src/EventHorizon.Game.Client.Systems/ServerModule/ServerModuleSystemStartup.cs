namespace EventHorizon.Game.Client.Systems;

using EventHorizon.Game.Client.Systems.ServerModule.Api;
using EventHorizon.Game.Client.Systems.ServerModule.State;

using Microsoft.Extensions.DependencyInjection;

public static class ServerModuleSystemStartup
{
    public static IServiceCollection AddServerModuleSystemServices(
        this IServiceCollection services
    ) =>
        services
            .AddSingleton<
                ServerModuleScriptsState,
                StandardServerModuleScriptsState
            >()
            .AddSingleton<ServerModuleState, StandardServerModuleState>();
}
