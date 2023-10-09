namespace EventHorizon.Game.Client.Engine.Particle.Api;

public interface ParticleTemplate
{
    string Id { get; }
    string Name { get; }
    string Type { get; }
    ParticleSettings DefaultSettings { get; }
}
