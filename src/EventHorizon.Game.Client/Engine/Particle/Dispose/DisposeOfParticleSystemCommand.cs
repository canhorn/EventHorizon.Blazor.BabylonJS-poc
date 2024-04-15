namespace EventHorizon.Game.Client.Engine.Particle.Dispose;

using EventHorizon.Game.Client.Core.Command.Model;
using MediatR;

public struct DisposeOfParticleSystemCommand : IRequest<StandardCommandResult>
{
    public long Id { get; }

    public DisposeOfParticleSystemCommand(long id)
    {
        Id = id;
    }
}
