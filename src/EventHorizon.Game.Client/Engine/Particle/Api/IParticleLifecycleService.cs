using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Lifecycle.Api;

namespace EventHorizon.Game.Client.Engine.Particle.Api
{
    public interface IParticleLifecycleService : IServiceEntity
    {
        Task StartModule(long id);
        Task StopModule(long id);
        Task DisposeModule(long id);
        Task UpdateModule(
            long id,
            IParticleSettings settings
        );
    }
}
