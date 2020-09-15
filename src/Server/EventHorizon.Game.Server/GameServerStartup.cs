namespace EventHorizon.Game.Server
{
    using EventHorizon.Game.Server.Game;
    using Microsoft.Extensions.DependencyInjection;

    public static class GameServerStartup
    {
        public static IServiceCollection AddGameServerServices(
            this IServiceCollection services
        ) => services
            .AddGameServerGameServices()
        ;
    }
}
