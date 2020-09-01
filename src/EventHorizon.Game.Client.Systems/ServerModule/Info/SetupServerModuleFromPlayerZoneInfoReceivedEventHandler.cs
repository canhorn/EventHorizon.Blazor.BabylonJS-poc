namespace EventHorizon.Game.Client.Systems.ServerModule.Info
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Systems.ServerModule.Add;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Info;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using System.Linq;

    public class SetupServerModuleFromPlayerZoneInfoReceivedEventHandler
        : INotificationHandler<PlayerZoneInfoReceivedEvent>
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public SetupServerModuleFromPlayerZoneInfoReceivedEventHandler(
            ILogger<SetupServerModuleFromPlayerZoneInfoReceivedEventHandler> logger,
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
            var serverModuleScriptsList = notification.PlayerZoneInfo.ServerModuleScriptList;

            _logger.LogInformation(
                "ServerModule: {ScriptCount}",
                serverModuleScriptsList.Count()
            );
            foreach (var serverModuleScript in serverModuleScriptsList)
            {
                var result = await _mediator.Send(
                    new AddServerModuleScriptCommand(
                        serverModuleScript
                    )
                );
                if (!result.Success)
                {
                    _logger.LogWarning(
                        "Failed to Add ServerModule Script: {ScriptName} | {ErrorCode}",
                        serverModuleScript.Name,
                        result.ErrorCode
                    );
                }
            }
        }
    }
}
