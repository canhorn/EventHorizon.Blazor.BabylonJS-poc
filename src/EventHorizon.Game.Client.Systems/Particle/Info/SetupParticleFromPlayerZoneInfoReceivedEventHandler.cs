namespace EventHorizon.Game.Client.Systems.Particle.Info
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Entity.Model;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Register;
    using EventHorizon.Game.Client.Engine.Particle.Add;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Info;
    using EventHorizon.Game.Client.Systems.Particle.Model;
    using MediatR;

    public class SetupParticleFromPlayerZoneInfoReceivedEventHandler
        : INotificationHandler<PlayerZoneInfoReceivedEvent>
    {
        private readonly IMediator _mediator;

        public SetupParticleFromPlayerZoneInfoReceivedEventHandler(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        public async Task Handle(
            PlayerZoneInfoReceivedEvent notification,
            CancellationToken cancellationToken
        )
        {
            foreach (var particleTemplate in notification.PlayerZoneInfo.ParticleTemplateList)
            {
                await _mediator.Send(
                    new AddParticleTemplateCommand(
                        particleTemplate
                    ),
                    cancellationToken
                );
            }
        }
    }
}
