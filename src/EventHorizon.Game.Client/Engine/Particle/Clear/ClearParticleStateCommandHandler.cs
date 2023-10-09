namespace EventHorizon.Game.Client.Engine.Particle.Clear;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Particle.Api;

using MediatR;

public class ClearParticleStateCommandHandler
    : IRequestHandler<ClearParticleStateCommand, StandardCommandResult>
{
    private readonly ParticleState _state;

    public ClearParticleStateCommandHandler(ParticleState state)
    {
        _state = state;
    }

    public async Task<StandardCommandResult> Handle(
        ClearParticleStateCommand request,
        CancellationToken cancellationToken
    )
    {
        await _state.Clear();

        return new();
    }
}
