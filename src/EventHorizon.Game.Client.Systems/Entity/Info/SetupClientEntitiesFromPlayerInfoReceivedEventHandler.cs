namespace EventHorizon.Game.Client.Systems.Entity.Info
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Register;
    using EventHorizon.Game.Client.Systems.ClientAssets.Loaded;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Info;
    using MediatR;

    public class SetupClientEntitiesFromPlayerInfoReceivedEventHandler
        : INotificationHandler<PlayerZoneInfoReceivedEvent>
    {
        private readonly IMediator _mediator;

        public SetupClientEntitiesFromPlayerInfoReceivedEventHandler(
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
            foreach (var clientEntityDetails in notification.PlayerZoneInfo.ClientEntityList)
            {
                await _mediator.Send(
                    new RegisterClientEntity(
                        clientEntityDetails
                    )
                );
            }
        }
    }
}
