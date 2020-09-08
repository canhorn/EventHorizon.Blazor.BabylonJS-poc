namespace EventHorizon.Game.Client.Engine.Particle.Add
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Engine.Particle.Api;
    using MediatR;

    public class AddParticleTemplateCommandHandler
        : IRequestHandler<AddParticleTemplateCommand, StandardCommandResult>
    {
        private readonly ParticleState _particleService;

        public AddParticleTemplateCommandHandler(
            ParticleState particleService
        )
        {
            _particleService = particleService;
        }

        public Task<StandardCommandResult> Handle(
            AddParticleTemplateCommand request,
            CancellationToken cancellationToken
        )
        {
            _particleService.AddTemplate(
                request.Template
            );

            return new StandardCommandResult()
                .FromResult();
        }
    }
}
