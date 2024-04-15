namespace EventHorizon.Game.Client.Systems.Combat.Skill.ClientAction;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Systems.Scripting.Run;
using MediatR;
using Microsoft.Extensions.Logging;

public class ClientActionRunSkillActionEventHandler
    : INotificationHandler<ClientActionRunSkillActionEvent>
{
    private readonly ILogger _logger;
    private readonly IMediator _mediator;

    public ClientActionRunSkillActionEventHandler(
        ILogger<ClientActionRunSkillActionEventHandler> logger,
        IMediator mediator
    )
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Handle(
        ClientActionRunSkillActionEvent notification,
        CancellationToken cancellationToken
    )
    {
        _logger.LogDebug(
            "Run Skill Action: {SkillAction} | {Data}",
            notification.Action,
            notification.Data
        );

        await _mediator.Send(
            new RunClientScriptCommand(notification.Action, notification.Action, notification.Data),
            cancellationToken
        );
    }
}
