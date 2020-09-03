namespace EventHorizon.Game.Client.Systems.Player
{
    using System;
    using EventHorizon.Game.Client.Core.Mapper.Api;
    using EventHorizon.Game.Client.Systems.Player.Api;
    using EventHorizon.Game.Client.Systems.Player.Mapper;
    using EventHorizon.Game.Client.Systems.Player.State;
    using EventHorizon.Game.Server.ServerModule.Game.Model;
    using Microsoft.Extensions.DependencyInjection;

    public static class PlayerSystemStartup
    {
        public static IServiceCollection AddPlayerSystemServices(
            this IServiceCollection services
        ) => services
            .AddSingleton<IMapper<IGamePlayerCaptureState>, GamePlayerCaptureStateMapper>()

            .AddSingleton<IPlayerState, StandardPlayerState>()
        ;
    }
}
