﻿namespace EventHorizon.Game.Client.Systems.Player
{
    using System;
    using EventHorizon.Game.Client.Core.Mapper.Api;
    using EventHorizon.Game.Client.Core.Mapper.Model;
    using EventHorizon.Game.Client.Systems.Player.Api;
    using EventHorizon.Game.Client.Systems.Player.Modules.SelectedCompanionTracker.Api;
    using EventHorizon.Game.Client.Systems.Player.Modules.SelectedCompanionTracker.Mapper;
    using EventHorizon.Game.Client.Systems.Player.State;
    using EventHorizon.Game.Server.ServerModule.Game.Model;
    using Microsoft.Extensions.DependencyInjection;

    public static class PlayerSystemStartup
    {
        public static IServiceCollection AddPlayerSystemServices(
            this IServiceCollection services
        ) => services
            .AddSingleton<IMapper<IGamePlayerCaptureState>, StandardMapper<IGamePlayerCaptureState, GamePlayerCaptureState>>()

            // Module Model Mappers
            .AddSingleton<IMapper<OwnerState>, StandardOwnerStateMapper>()

            .AddSingleton<IPlayerState, StandardPlayerState>()
        ;
    }
}
