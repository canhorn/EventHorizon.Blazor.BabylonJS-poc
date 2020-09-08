namespace EventHorizon.Game.Client.Engine.Particle.Dispose
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Engine.Particle.Api;
    using MediatR;

    public class DisposeOfParticleSystemCommandHandler
        : IRequestHandler<DisposeOfParticleSystemCommand, StandardCommandResult>
    {

        private readonly ParticleLifecycleService _particleLifecycleService;

        public DisposeOfParticleSystemCommandHandler(
            ParticleLifecycleService particleLifecycleService
        )
        {
            _particleLifecycleService = particleLifecycleService;
        }

        public async Task<StandardCommandResult> Handle(
            DisposeOfParticleSystemCommand request, 
            CancellationToken cancellationToken
        )
        {
            await _particleLifecycleService.DisposeSystem(
                request.Id
            );

            return new StandardCommandResult();
        }
    }
}
