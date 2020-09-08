namespace EventHorizon.Game.Client.Engine.Particle.Api
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Lifecycle.Api;

    public interface ParticleState
        : IServiceEntity
    {
        Task AddTemplate(
            ParticleTemplate template
        );
        Task CreateFromTemplate(
            long id,
            string templateId,
            ParticleSettings settings
        );
    }
}
