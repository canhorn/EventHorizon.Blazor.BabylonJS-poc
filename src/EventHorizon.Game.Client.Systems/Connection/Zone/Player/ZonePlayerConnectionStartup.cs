namespace EventHorizon.Game.Client.Systems.Connection.Zone.Player
{
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Api;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.State;
    using Microsoft.Extensions.DependencyInjection;

    public static class ZonePlayerConnectionStartup
    {
        public static IServiceCollection AddZonePlayerConnectionServices(
            this IServiceCollection services
        ) => services
            .AddSingleton<IZonePlayerConnectionState, SignalRZonePlayerConnectionState>()
        ;
    }
}
