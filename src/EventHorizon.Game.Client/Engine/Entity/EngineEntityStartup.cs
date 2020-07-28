namespace EventHorizon.Game.Client.Engine.Entity
{
    using EventHorizon.Game.Client.Core.Builder.Api;
    using EventHorizon.Game.Client.Engine.Entity.Api;
    using EventHorizon.Game.Client.Engine.Entity.Builder;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using Microsoft.Extensions.DependencyInjection;

    public static class EngineEntityStartup
    {
        public static IServiceCollection AddEngineEntityServices(
            this IServiceCollection services
        ) => services
            .AddSingleton<IBuilder<ITransform, IServerTransform>, TransformBuilder>()
            .AddSingleton<IBuilder<IVector3, IServerVector3>, BabylonJSVector3Builder>()
        ;
    }
}
