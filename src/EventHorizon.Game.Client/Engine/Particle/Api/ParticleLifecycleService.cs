namespace EventHorizon.Game.Client.Engine.Particle.Api;

using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Lifecycle.Api;

public interface ParticleLifecycleService : IServiceEntity
{
    Task StartSystem(long id);
    Task StopSystem(long id);
    Task DisposeSystem(long id);
    Task UpdateSystem(long id, ParticleSettings settings);
}
