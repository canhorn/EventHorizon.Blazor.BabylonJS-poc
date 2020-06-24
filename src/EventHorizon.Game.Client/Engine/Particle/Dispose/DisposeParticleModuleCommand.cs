using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace EventHorizon.Game.Client.Engine.Particle.Dispose
{
    public struct DisposeParticleModuleCommand
        : IRequest
    {
        public long Id { get; }

        public DisposeParticleModuleCommand(
            long id
        )
        {
            Id = id;
        }
    }
}
