namespace EventHorizon.Game.Client.Engine.Particle.Add
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Particle.Api;
    using MediatR;

    public class AddParticleTemplateCommandHandler
        : IRequestHandler<AddParticleTemplateCommand>
    {
        private readonly IParticleService _particleService;

        public AddParticleTemplateCommandHandler(
            IParticleService particleService
        )
        {
            _particleService = particleService;
        }

        public Task<Unit> Handle(
            AddParticleTemplateCommand request, 
            CancellationToken cancellationToken
        )
        {
            _particleService.AddTemplate(
                request.Template
            );

            return Unit.Task;
        }
    }
}
