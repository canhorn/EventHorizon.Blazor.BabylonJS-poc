namespace EventHorizon.Game.Client.Systems.Player
{
    using System;
    using EventHorizon.Game.Client.Core.Mapper.Api;
    using EventHorizon.Game.Client.Core.Mapper.Model;
    using EventHorizon.Game.Client.Systems.Player.Api;
    using EventHorizon.Game.Client.Systems.Player.Modules.SelectedCompanionTracker.Api;
    using EventHorizon.Game.Client.Systems.Player.Modules.SelectedCompanionTracker.Model;
    using EventHorizon.Game.Client.Systems.Player.State;
    using Microsoft.Extensions.DependencyInjection;

    public static class PlayerSystemStartup
    {
        public static IServiceCollection AddPlayerSystemServices(
            this IServiceCollection services
        ) => services
            // Module Model Mappers
            .AddSingleton<IMapper<OwnerState>, StandardMapper<OwnerState, StandardOwnerState>>()

            .AddSingleton<IPlayerState, StandardPlayerState>()
        ;
    }
}
