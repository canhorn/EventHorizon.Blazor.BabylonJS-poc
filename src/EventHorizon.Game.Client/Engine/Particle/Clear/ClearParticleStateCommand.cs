namespace EventHorizon.Game.Client.Engine.Particle.Clear;

using EventHorizon.Game.Client.Core.Command.Model;

using MediatR;

public record ClearParticleStateCommand
    : IRequest<StandardCommandResult>;
