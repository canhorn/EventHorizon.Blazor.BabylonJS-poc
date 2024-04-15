namespace EventHorizon.Game.Client.Engine.Particle.Start;

using System;
using EventHorizon.Game.Client.Core.Command.Model;
using MediatR;

public struct StartParticleSystemCommand : IRequest<StandardCommandResult>
{
    public long Id { get; }

    public StartParticleSystemCommand(long id)
    {
        Id = id;
    }
}
