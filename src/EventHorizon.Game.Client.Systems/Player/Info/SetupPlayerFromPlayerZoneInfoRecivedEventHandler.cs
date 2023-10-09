namespace EventHorizon.Game.Client.Systems.Player.Info;

using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Info;
using EventHorizon.Game.Client.Systems.Player.Register;

using MediatR;

public class SetupPlayerFromPlayerZoneInfoRecivedEventHandler
    : INotificationHandler<PlayerZoneInfoReceivedEvent>
{
    private readonly IMediator _mediator;

    public SetupPlayerFromPlayerZoneInfoRecivedEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(
        PlayerZoneInfoReceivedEvent notification,
        CancellationToken cancellationToken
    )
    {
        await _mediator.Send(
            new RegisterPlayerCommand(notification.PlayerZoneInfo.Player),
            cancellationToken
        );
    }
}
