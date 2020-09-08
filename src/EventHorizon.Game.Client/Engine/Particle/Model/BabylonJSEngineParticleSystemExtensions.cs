namespace EventHorizon.Game.Client.Engine.Particle.Model
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using BabylonJS;
    using EventHorizon.Game.Client.Engine.Particle.Api;

    public static class BabylonJSEngineParticleSystemExtensions
    {
        public static Option<ParticleSystem> GetBabylonJSParticleSystem(
            this EngineParticleSystem system
        )
        {
            if (system is BabylonJSEngineParticleSystem babylonParticleSystem)
            {
                return new Option<ParticleSystem>(
                    babylonParticleSystem.ParticleSystem
                );
            }
            return new Option<ParticleSystem>(
                null
            );
        }
    }
}
