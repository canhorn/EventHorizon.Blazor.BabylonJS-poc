namespace EventHorizon.Game.Client.Systems.ClientScripts.Info
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Systems.ClientScripts.Load;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Info;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class SetupClientScriptsFromZoneInfoReceivedEventHandler
        : INotificationHandler<PlayerZoneInfoReceivedEvent>
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public SetupClientScriptsFromZoneInfoReceivedEventHandler(
            ILogger<SetupClientScriptsFromZoneInfoReceivedEventHandler> logger,
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
            var hash = notification.PlayerZoneInfo.ClientScriptsAssemblyDetails.Hash;
            var result = await _mediator.Send(
                new LoadClientScriptsAssembly(
                    hash
                )
            );
            if (!result.Success)
            {
                _logger.LogError(
                    "Failed to load ClientScriptAssembly: {Hash} | {ErrorCode}",
                    hash,
                    result.ErrorCode
                );
            }
        }
    }
}
