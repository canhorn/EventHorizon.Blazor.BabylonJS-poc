namespace EventHorizon.Game.Client.Engine.Particle.Api;

using System.Threading.Tasks;

public interface EngineParticleSystem
{
    long Id { get; }
    Task Start();
    Task Stop();
    Task Dispose();
    Task UpdateSettings(ParticleSettings settings);
}
