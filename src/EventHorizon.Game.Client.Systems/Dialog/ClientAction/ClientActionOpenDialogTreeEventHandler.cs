namespace EventHorizon.Game.Client.Systems.Dialog.ClientAction
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Entity.Tag;
    using EventHorizon.Game.Client.Engine.Entity.Tracking.Query;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Client.Systems.Dialog.Api;
    using EventHorizon.Game.Client.Systems.Dialog.Open;
    using EventHorizon.Game.Client.Systems.Dialog.Query;
    using EventHorizon.Game.Client.Systems.Player.Api;
    using EventHorizon.Game.Client.Systems.Player.Query;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class ClientActionOpenDialogTreeEventHandler
        : INotificationHandler<ClientActionOpenDialogTreeEvent>
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public ClientActionOpenDialogTreeEventHandler(
            ILogger<ClientActionOpenDialogTreeEventHandler> logger,
            IMediator mediator
        )
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Handle(
            ClientActionOpenDialogTreeEvent notification,
            CancellationToken cancellationToken
        )
        {
            var result = await _mediator.Send(
                new OpenDialogTreeCommand(
                    notification.DialogTreeId,
                    notification.PlayerId,
                    notification.NpcId
                ),
                cancellationToken
            );
            if (!result.Success)
            {
                _logger.LogError(
                    "Failed to Open Dialog Tree, Reason: {ErrorCode}",
                    result.ErrorCode
                );
            }
        }
    }
}