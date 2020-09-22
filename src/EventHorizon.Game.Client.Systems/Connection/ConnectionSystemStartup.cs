namespace EventHorizon.Game.Client.Systems
{
    using EventHorizon.Game.Client.Systems.Connection.Core;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player;
    using Microsoft.Extensions.DependencyInjection;

    public static class ConnectionSystemStartup
    {
        public static IServiceCollection AddConnectionSystemServices(
            this IServiceCollection services
        ) => services
            .AddCoreConnectionServices()

            .AddPlayerZoneConnectionServices()
        ;
    }
}
