namespace EventHorizon.Game.Client.Engine.Systems.Entity
{
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.Entity.State;
    using Microsoft.Extensions.DependencyInjection;

    public static class EntitySystemStartup
    {
        public static IServiceCollection AddEntitySystemServices(
            this IServiceCollection services
        ) => services
            .AddSingleton<IEntityDetailsState, StandardEntityDetailsState>()
        ;
    }
}
