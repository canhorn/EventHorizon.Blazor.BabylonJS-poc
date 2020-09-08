namespace EventHorizon.Game.Client.Engine.Particle.Api
{
    using System;

    public interface ParticleSettingsIntoParticleSystemMapper
    {
        void Map(
            EngineParticleSystem system,
            ParticleSettings settings
        );
    }
}
