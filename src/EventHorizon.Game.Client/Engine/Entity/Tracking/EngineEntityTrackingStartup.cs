namespace EventHorizon.Game.Client.Engine.Entity.Tracking
{
    using EventHorizon.Game.Client.Engine.Entity.Tracking.Api;
    using EventHorizon.Game.Client.Engine.Entity.Tracking.State;
    using Microsoft.Extensions.DependencyInjection;

    public static class EngineEntityTrackingStartup
    {
        public static IServiceCollection AddEngineEntityTrackingServices(
            this IServiceCollection services
        ) => services
            .AddSingleton<IServerEntityTrackingState, StandardServerEntityTrackingState>()
        ;
    }
}
