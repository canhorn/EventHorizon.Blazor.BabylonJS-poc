namespace EventHorizon.Game.Server.Game
{
    using System;
    using EventHorizon.Game.Server.Game.Api;
    using EventHorizon.Game.Server.Game.Model;
    using Microsoft.Extensions.DependencyInjection;
    using EventHorizon.Game.Client.Core.Mapper.Api;
    using EventHorizon.Game.Client.Core.Mapper.Model;
    using EventHorizon.Game.Server.Game.State;

    public static class GameServerGameStartup
    {
        public static IServiceCollection AddGameServerGameServices(
            this IServiceCollection services
        ) => services
            .AddSingleton<IMapper<GameState>, StandardMapper<GameState, GameStateModel>>()
            .AddSingleton<IMapper<GamePlayerCaptureState>, StandardMapper<GamePlayerCaptureState, GamePlayerCaptureStateModel>>()

            .AddSingleton<ServerGameState, StandardServerGameState>()
        ;
    }
}
