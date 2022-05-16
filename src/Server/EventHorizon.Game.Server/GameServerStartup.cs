namespace EventHorizon.Game.Server;

using Microsoft.Extensions.DependencyInjection;

public static class GameServerStartup
{
    public static IServiceCollection AddGameServerServices(
        this IServiceCollection services
    ) => services;
}
