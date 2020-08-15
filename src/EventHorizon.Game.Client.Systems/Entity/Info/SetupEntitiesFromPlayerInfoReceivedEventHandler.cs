namespace EventHorizon.Game.Client.Systems.Entity.Info
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Configuration;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Register;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Register;
    using EventHorizon.Game.Client.Engine.Systems.Player.Api;
    using EventHorizon.Game.Client.Systems.ClientAssets.Loaded;
    using EventHorizon.Game.Client.Systems.Connection.Core.Model;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Info;
    using EventHorizon.Game.Client.Systems.Entity.Model;
    using MediatR;

    public class SetupEntitiesFromPlayerInfoReceivedEventHandler
        : INotificationHandler<PlayerZoneInfoReceivedEvent>
    {
        private readonly IMediator _mediator;

        public SetupEntitiesFromPlayerInfoReceivedEventHandler(
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
            // TODO: Put this under the Player System into IPlayerState 
            var playerDetails = Configuration.GetConfig<IPlayerDetails>(
                "PLAYER_DETAILS"
            );
            System.Console.WriteLine($"PlayerId: {playerDetails.PlayerId}");
            foreach (var entityDetails in notification.PlayerZoneInfo.EntityList)
            {
                System.Console.WriteLine($"GlobalId: {entityDetails.GlobalId}");
                if (playerDetails.PlayerId != entityDetails.GlobalId)
                {
                    System.Console.WriteLine($"Register Entity");
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
