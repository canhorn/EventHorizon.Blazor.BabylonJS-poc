namespace EventHorizon.Game.Client.Engine.Particle.Stop
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Engine.Particle.Api;
    using MediatR;

    public class StopParticleSystemCommandHandler
        : IRequestHandler<StopParticleSystemCommand, StandardCommandResult>
    {
        private readonly ParticleLifecycleService _service;

        public StopParticleSystemCommandHandler(
            ParticleLifecycleService service
        )
        {
            _service = service;
        }

        public async Task<StandardCommandResult> Handle(
            StopParticleSystemCommand request,
            CancellationToken cancellationToken
        )
        {
            await _service.StopSystem(
                request.Id
            );

            return new StandardCommandResult();
        }
    }
}
