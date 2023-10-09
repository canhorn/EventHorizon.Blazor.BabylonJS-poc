namespace EventHorizon.Game.Client.Engine.Particle.Update;

using System;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Particle.Api;

using MediatR;

public struct UpdateParticleSystemCommand : IRequest<StandardCommandResult>
{
    public long Id { get; }
    public ParticleSettings Settings { get; }

    public UpdateParticleSystemCommand(long id, ParticleSettings settings)
    {
        Id = id;
        Settings = settings;
    }
}
