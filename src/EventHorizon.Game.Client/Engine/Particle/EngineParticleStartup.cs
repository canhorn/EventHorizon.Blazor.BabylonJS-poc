namespace EventHorizon.Game.Client.Engine.Particle;

using EventHorizon.Game.Client.Engine.Particle.Api;
using EventHorizon.Game.Client.Engine.Particle.Mapper;
using EventHorizon.Game.Client.Engine.Particle.State;
using Microsoft.Extensions.DependencyInjection;

public static class EngineParticleStartup
{
    public static IServiceCollection AddEngineParticleServices(this IServiceCollection services) =>
        services
            .AddSingleton<
                ParticleSettingsIntoParticleSystemMapper,
                BabylonJSParticleSettingsIntoParticleSystemMapper
            >()
            // TODO: Might need to split this up
            // Could dispose of the BabylonJSParticleState, based on many times it is registered with DI
            .AddSingleton<BabylonJSParticleState>()
            .AddSingleton<ParticleState>(services =>
                services.GetRequiredService<BabylonJSParticleState>()
            )
            .AddSingleton<ParticleLifecycleService>(services =>
                services.GetRequiredService<BabylonJSParticleState>()
            );
}
