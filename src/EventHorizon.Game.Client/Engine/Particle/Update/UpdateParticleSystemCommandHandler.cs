namespace EventHorizon.Game.Client.Engine.Particle.Update;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Particle.Api;
using MediatR;

public class UpdateParticleSystemCommandHandler
    : IRequestHandler<UpdateParticleSystemCommand, StandardCommandResult>
{
    private readonly ParticleLifecycleService _service;

    public UpdateParticleSystemCommandHandler(ParticleLifecycleService service)
    {
        _service = service;
    }

    public async Task<StandardCommandResult> Handle(
        UpdateParticleSystemCommand request,
        CancellationToken cancellationToken
    )
    {
        await _service.UpdateSystem(request.Id, request.Settings);

        return new StandardCommandResult();
    }
}
