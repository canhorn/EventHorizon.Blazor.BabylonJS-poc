namespace EventHorizon.Game.Client.Systems;

using EventHorizon.Game.Client.Systems.Local.Scenes.Api;
using EventHorizon.Game.Client.Systems.Local.Scenes.State;

using Microsoft.Extensions.DependencyInjection;

public static class ClientScenesStartup
{
    public static IServiceCollection AddClientScenesServices(
        this IServiceCollection services
    ) =>
        services.AddSingleton<
            ISceneOrchestratorState,
            StandardSceneOrchestratorState
        >();
}
