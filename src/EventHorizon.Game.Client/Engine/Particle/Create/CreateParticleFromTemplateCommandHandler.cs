namespace EventHorizon.Game.Client.Engine.Particle.Create
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Particle.Api;
    using MediatR;

    public class CreateParticleFromTemplateCommandHandler
        : IRequestHandler<CreateParticleFromTemplateCommand>
    {
        private readonly IParticleService _particleService;

        public CreateParticleFromTemplateCommandHandler(
            IParticleService particleService
        )
        {
            _particleService = particleService;
        }

        public async Task<Unit> Handle(
            CreateParticleFromTemplateCommand request,
            CancellationToken cancellationToken
        )
        {
            await _particleService.CreateFromTemplate(
                request.Id,
                request.TemplateId,
                request.Settings
            );

            return Unit.Value;
        }
    }
}
