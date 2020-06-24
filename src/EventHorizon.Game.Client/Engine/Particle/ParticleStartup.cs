using System;
using System.Collections.Generic;
using System.Text;
using EventHorizon.Game.Client.Engine.Particle.Api;
using Microsoft.Extensions.DependencyInjection;

namespace EventHorizon.Game.Client.Engine.Particle
{
    public static class ParticleStartup
    {
        public static IServiceCollection AddParticleServices(
            this IServiceCollection services
        ) => services
        //.AddSingleton<StandardParticleService>()
        //.AddSingleton<IParticleService>(services => services.GetService<StandardParticleService>())
        //.AddSingleton<IParticleLifecycleService>(services => services.GetService<StandardParticleService>())
        ;
    }
}
