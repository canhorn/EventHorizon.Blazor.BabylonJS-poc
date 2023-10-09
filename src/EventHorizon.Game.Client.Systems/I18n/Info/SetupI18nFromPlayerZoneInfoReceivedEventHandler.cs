namespace EventHorizon.Game.Client.Systems.I18n.Info;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.I18n.Set;
using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Info;

using MediatR;

public class SetupI18nFromPlayerZoneInfoReceivedEventHandler
    : INotificationHandler<PlayerZoneInfoReceivedEvent>
{
    private readonly IMediator _mediator;

    public SetupI18nFromPlayerZoneInfoReceivedEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(
        PlayerZoneInfoReceivedEvent notification,
        CancellationToken cancellationToken
    )
    {
        await _mediator.Send(
            new SetI18nBundleCommand(notification.PlayerZoneInfo.I18nMap),
            cancellationToken
        );
    }
}
