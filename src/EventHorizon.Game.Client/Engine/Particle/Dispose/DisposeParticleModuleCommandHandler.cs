namespace EventHorizon.Game.Client.Engine.Particle.Dispose
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Particle.Api;
    using MediatR;

    public class DisposeParticleModuleCommandHandler
        : IRequestHandler<DisposeParticleModuleCommand>
    {

        private readonly IParticleLifecycleService _particleLifecycleService;

        public DisposeParticleModuleCommandHandler(
            IParticleLifecycleService particleLifecycleService
        )
        {
            _particleLifecycleService = particleLifecycleService;
        }

        public async Task<Unit> Handle(
            DisposeParticleModuleCommand request, 
            CancellationToken cancellationToken
        )
        {
            await _particleLifecycleService.DisposeModule(
                request.Id
            );

            return Unit.Value;
        }
    }
}
