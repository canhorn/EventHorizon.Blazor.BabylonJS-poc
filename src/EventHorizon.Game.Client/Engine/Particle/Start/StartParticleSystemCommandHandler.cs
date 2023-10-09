namespace EventHorizon.Game.Client.Engine.Particle.Start;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Particle.Api;

using MediatR;

public class StartParticleSystemCommandHandler
    : IRequestHandler<StartParticleSystemCommand, StandardCommandResult>
{
    private readonly ParticleLifecycleService _service;

    public StartParticleSystemCommandHandler(ParticleLifecycleService service)
    {
        _service = service;
    }

    public async Task<StandardCommandResult> Handle(
        StartParticleSystemCommand request,
        CancellationToken cancellationToken
    )
    {
        await _service.StartSystem(request.Id);

        return new StandardCommandResult();
    }
}
