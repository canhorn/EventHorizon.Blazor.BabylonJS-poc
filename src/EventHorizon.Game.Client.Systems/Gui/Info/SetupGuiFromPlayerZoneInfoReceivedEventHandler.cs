namespace EventHorizon.Game.Client.Systems.Gui.Info;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Gui.Register;
using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Info;

using MediatR;

public class SetupGuiFromPlayerZoneInfoReceivedEventHandler
    : INotificationHandler<PlayerZoneInfoReceivedEvent>
{
    private readonly IMediator _mediator;

    public SetupGuiFromPlayerZoneInfoReceivedEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(
        PlayerZoneInfoReceivedEvent notification,
        CancellationToken cancellationToken
    )
    {
        foreach (var guiLayout in notification.PlayerZoneInfo.GuiLayoutList)
        {
            await _mediator.Send(new RegisterGuiLayoutDataCommand(guiLayout));
        }
    }
}
