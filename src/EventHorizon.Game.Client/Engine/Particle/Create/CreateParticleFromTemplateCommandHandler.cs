namespace EventHorizon.Game.Client.Engine.Particle.Create;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Particle.Api;

using MediatR;

public class CreateParticleFromTemplateCommandHandler
    : IRequestHandler<CreateParticleFromTemplateCommand, StandardCommandResult>
{
    private readonly ParticleState _particleService;

    public CreateParticleFromTemplateCommandHandler(
        ParticleState particleService
    )
    {
        _particleService = particleService;
    }

    public async Task<StandardCommandResult> Handle(
        CreateParticleFromTemplateCommand request,
        CancellationToken cancellationToken
    )
    {
        await _particleService.CreateFromTemplate(
            request.Id,
            request.TemplateId,
            request.Settings
        );

        return new StandardCommandResult();
    }
}
