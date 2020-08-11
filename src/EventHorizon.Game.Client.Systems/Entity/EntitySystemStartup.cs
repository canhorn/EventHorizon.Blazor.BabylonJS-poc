namespace EventHorizon.Game.Client.Systems.Entity
{
    using System;
    using EventHorizon.Game.Client.Core.Mapper.Api;
    using EventHorizon.Game.Client.Systems.Entity.Properties.Model.Api;
    using EventHorizon.Game.Client.Systems.Entity.Properties.Model.Mapper;
    using EventHorizon.Game.Client.Systems.Entity.Properties.Move.Api;
    using EventHorizon.Game.Client.Systems.Entity.Properties.Move.Mapper;
    using Microsoft.Extensions.DependencyInjection;

    public static class EntitySystemStartup
    {
        public static IServiceCollection AddEntitySystemServices(
            this IServiceCollection services
        ) => services
            .AddSingleton<IMapper<IModelState>, StandardModelStateMapper>()
            .AddSingleton<IMapper<IMovementState>, StandardMovementStateMapper>()
        ;
    }
}
