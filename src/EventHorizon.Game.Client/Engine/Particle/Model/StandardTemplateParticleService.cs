using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Entity.Model;
using EventHorizon.Game.Client.Engine.Lifecycle.Register.Api;
using EventHorizon.Game.Client.Engine.Particle.Api;

namespace EventHorizon.Game.Client.Engine.Particle.Model
{
    public class StandardTemplateParticleService
        : IParticleService, IParticleLifecycleService
    {
        public int Priority => 0;

        public StandardTemplateParticleService()
        {
        }

        public Task Initialize()
        {
            throw new NotImplementedException();
        }

        public Task Dispose()
        {
            throw new NotImplementedException();
        }


        public Task AddTemplate(IParticleTemplate template)
        {
            throw new NotImplementedException();
        }

        public Task CreateFromTemplate(long id, string templateId, IParticleSettings settings)
        {
            throw new NotImplementedException();
        }

        public Task DisposeModule(long id)
        {
            throw new NotImplementedException();
        }

        public Task StartModule(long id)
        {
            throw new NotImplementedException();
        }

        public Task StopModule(long id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateModule(long id, IParticleSettings settings)
        {
            throw new NotImplementedException();
        }
    }
}
