namespace EventHorizon.Game.Client.Engine.Particle.Model;

using System.Threading.Tasks;

using BabylonJS;

using EventHorizon.Game.Client.Engine.Particle.Api;
using EventHorizon.Game.Client.Engine.Rendering.Api;

public class BabylonJSEngineParticleSystem : EngineParticleSystem
{
    private readonly ParticleSettingsIntoParticleSystemMapper _mapper =
        GameServiceProvider.GetService<ParticleSettingsIntoParticleSystemMapper>();

    public long Id { get; }
    public ParticleSystem ParticleSystem { get; }

    public BabylonJSEngineParticleSystem(long id, ParticleSettings settings)
    {
        Id = id;
        ParticleSystem = new ParticleSystem(
            settings.Name,
            settings.Capacity,
            GameServiceProvider
                .GetService<IRenderingScene>()
                .GetBabylonJSScene()
                .Scene
        );
        _mapper.Map(this, settings);
    }

    public Task Start()
    {
        ParticleSystem.start();
        return Task.CompletedTask;
    }

    public Task Stop()
    {
        ParticleSystem.stop();
        return Task.CompletedTask;
    }

    public Task Dispose()
    {
        ParticleSystem.dispose(true);
        return Task.CompletedTask;
    }

    public Task UpdateSettings(ParticleSettings settings)
    {
        _mapper.Map(this, settings);
        return Task.CompletedTask;
    }
}
