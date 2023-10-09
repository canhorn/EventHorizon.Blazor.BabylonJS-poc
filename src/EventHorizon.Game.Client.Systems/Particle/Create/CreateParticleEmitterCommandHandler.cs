namespace EventHorizon.Game.Client.Systems.Particle.Create;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Lifecycle.Register.Register;
using EventHorizon.Game.Client.Engine.Particle.Create;
using EventHorizon.Game.Client.Systems.Particle.Api;
using EventHorizon.Game.Client.Systems.Particle.Model;

using MediatR;

public class CreateParticleEmitterCommandHandler
    : IRequestHandler<
        CreateParticleEmitterCommand,
        CommandResult<ParticleEmitter>
    >
{
    private readonly IMediator _mediator;

    public CreateParticleEmitterCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<CommandResult<ParticleEmitter>> Handle(
        CreateParticleEmitterCommand request,
        CancellationToken cancellationToken
    )
    {
        var particleEmitter = new StandardParticleEmitter(
            request.TemplateId,
            request.Position,
            request.Speed
        );

        await _mediator.Publish(
            new RegisterEntityEvent(particleEmitter),
            cancellationToken
        );

        return new CommandResult<ParticleEmitter>(particleEmitter);
    }
}
