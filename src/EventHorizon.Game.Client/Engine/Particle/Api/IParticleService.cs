using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Lifecycle.Api;

namespace EventHorizon.Game.Client.Engine.Particle.Api
{
    public interface IParticleService : IServiceEntity
    {
        Task CreateFromTemplate(
            long id,
            string templateId,
            IParticleSettings settings
        );
        Task AddTemplate(
            IParticleTemplate template
        );
    }
}
