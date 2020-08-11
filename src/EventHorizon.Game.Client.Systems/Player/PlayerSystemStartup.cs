namespace EventHorizon.Game.Client.Systems.Player
{
    using System;
    using EventHorizon.Game.Client.Systems.Player.Api;
    using EventHorizon.Game.Client.Systems.Player.State;
    using Microsoft.Extensions.DependencyInjection;

    public static class PlayerSystemStartup
    {
        public static IServiceCollection AddPlayerSystemServices(
            this IServiceCollection services
        ) => services
            .AddSingleton<IPlayerState, StandardPlayerState>()
        ;
    }
}
