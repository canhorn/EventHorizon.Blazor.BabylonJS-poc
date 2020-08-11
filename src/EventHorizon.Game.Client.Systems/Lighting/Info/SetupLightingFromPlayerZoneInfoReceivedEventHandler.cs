namespace EventHorizon.Game.Client.Systems.Lighting.Info
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Info;
    using EventHorizon.Game.Client.Systems.Lighting.Create;
    using EventHorizon.Game.Client.Systems.Lighting.Model;
    using MediatR;

    public class SetupLightingFromPlayerZoneInfoReceivedEventHandler
        : INotificationHandler<PlayerZoneInfoReceivedEvent>
    {
        private readonly IMediator _mediator;

        public SetupLightingFromPlayerZoneInfoReceivedEventHandler(
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
            // TODO: [LIGHTING] : Load Global Lighting, from server
            // notification.PlayerZoneInfo.Lighting
            var enableDayNightCycle = false;
            await _mediator.Send(
                new CreateLightCommand(
                    new LightDetailsModel
                    {
                        Name = "global_light",
                        EnableDayNightCycle = enableDayNightCycle,
                        Type = "point",
                    }
                )
            );
            await _mediator.Send(
                new CreateLightCommand(
                    new LightDetailsModel
                    {
                        Name = "global_light_hem",
                        EnableDayNightCycle = enableDayNightCycle,
                        Type = "hemispheric",
                    }
                )
            );
        }
    }
}
