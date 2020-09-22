namespace EventHorizon.Game.Client.Systems
{
    using System;
    using EventHorizon.Game.Client.Core.Mapper.Api;
    using EventHorizon.Game.Client.Core.Mapper.Model;
    using EventHorizon.Game.Client.Systems.Entity.Modules.InteractionIndicator.Api;
    using EventHorizon.Game.Client.Systems.Entity.Modules.InteractionIndicator.Model;
    using EventHorizon.Game.Client.Systems.Entity.Properties.Model.Api;
    using EventHorizon.Game.Client.Systems.Entity.Properties.Model.Model;
    using EventHorizon.Game.Client.Systems.Entity.Properties.Move.Api;
    using EventHorizon.Game.Client.Systems.Entity.Properties.Move.Model;
    using Microsoft.Extensions.DependencyInjection;

    public static class EntitySystemStartup
    {
        public static IServiceCollection AddEntitySystemServices(
            this IServiceCollection services
        ) => services
            .AddSingleton<IMapper<IModelState>, StandardMapper<IModelState, ModelStateModel>>()
            .AddSingleton<IMapper<IMovementState>, StandardMapper<IMovementState, MovementStateModel>>()
            .AddSingleton<IMapper<InteractionState>, StandardMapper<InteractionState, InteractionStateModel>>()
        ;
    }
}
