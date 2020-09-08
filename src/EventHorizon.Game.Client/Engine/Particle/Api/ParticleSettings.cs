namespace EventHorizon.Game.Client.Engine.Particle.Api
{
    using System.Collections.Generic;

    public interface ParticleSettings
        : IDictionary<string, object>
    {
        string Name { get; }
        decimal Capacity { get; }
    }
}