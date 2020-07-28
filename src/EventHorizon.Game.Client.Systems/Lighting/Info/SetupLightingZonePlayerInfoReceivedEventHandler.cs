namespace EventHorizon.Game.Client.Systems.Lighting.Info
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Info;
    using EventHorizon.Game.Client.Systems.Lighting.Create;
    using EventHorizon.Game.Client.Systems.Lighting.Model;
    using MediatR;

    public class SetupLightingZonePlayerInfoReceivedEventHandler
        : INotificationHandler<ZonePlayerInfoReceivedEvent>
    {
        private readonly IMediator _mediator;

        public SetupLightingZonePlayerInfoReceivedEventHandler(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        public async Task Handle(
            ZonePlayerInfoReceivedEvent notification, 
            CancellationToken cancellationToken
        )
        {
            // TODO: [LIGHTING] : Load Global Lighting, from server
            // notification.ZonePlayerInfo.Lighting
            await _mediator.Send(
                new CreateLightCommand(
                    new LightDetailsModel
                    {
                        Name = "global_light",
                        EnableDayNightCycle = true,
                        Type = "point",
                    }
                )
            );
            await _mediator.Send(
                new CreateLightCommand(
                    new LightDetailsModel
                    {
                        Name = "global_light_hem",
                        EnableDayNightCycle = true,
                        Type = "hemispheric",
                    }
                )
            );
        }
    }
}
