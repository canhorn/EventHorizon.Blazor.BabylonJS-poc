namespace EventHorizon.Game.Client.Systems.Connection.Zone.Player
{
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Api;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.State;
    using Microsoft.Extensions.DependencyInjection;

    public static class PlayerZoneConnectionStartup
    {
        public static IServiceCollection AddPlayerZoneConnectionServices(
            this IServiceCollection services
        ) => services
            .AddSingleton<IPlayerZoneConnectionState, SignalRPlayerZoneConnectionState>()
        ;
    }
}
