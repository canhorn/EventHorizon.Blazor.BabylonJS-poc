namespace EventHorizon.Game.Client.Systems.Particle.Modules.ParticleEmitter.Api
{
    using System;
    using EventHorizon.Game.Client.Engine.Systems.Module.Api;

    public interface ParticleEmitterModule
        : IModule
    {
        public static string MODULE_NAME => "PARTICLE_EMITTER_MODULE_NAME";
    }
}
