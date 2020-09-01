namespace EventHorizon.Game.Client.Systems.Entity.Info
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Configuration;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Register;
    using EventHorizon.Game.Client.Engine.Systems.Player.Api;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Info;
    using EventHorizon.Game.Client.Systems.Entity.Model;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class SetupEntitiesFromPlayerInfoReceivedEventHandler
        : INotificationHandler<PlayerZoneInfoReceivedEvent>
    {
        private readonly ILogger<SetupEntitiesFromPlayerInfoReceivedEventHandler> _logger;
        private readonly IMediator _mediator;

        public SetupEntitiesFromPlayerInfoReceivedEventHandler(
            ILogger<SetupEntitiesFromPlayerInfoReceivedEventHandler> logger,
            IMediator mediator
        )
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Handle(
            PlayerZoneInfoReceivedEvent notification,
            CancellationToken cancellationToken
        )
        {
            // TODO: Put this under the Player System into IPlayerState 
            var playerDetails = Configuration.GetConfig<IPlayerDetails>(
                "PLAYER_DETAILS"
            );
            _logger.LogInformation(
                "PlayerId: {PlayerId}",
                playerDetails.PlayerId
            );
            foreach (var entityDetails in notification.PlayerZoneInfo.EntityList)
            {
                _logger.LogInformation(
                    "GlobalId: {GlobalId}",
                    entityDetails.GlobalId
                );
                if (playerDetails.PlayerId != entityDetails.GlobalId)
                {
                    _logger.LogInformation(
                        "Register Entity"
                    );
                    await _mediator.Publish(
                        new RegisterEntityEvent(
                            new StandardServerEntity(
                                entityDetails
                            )
                        )
                    );
                }
            }
        }
    }
}
