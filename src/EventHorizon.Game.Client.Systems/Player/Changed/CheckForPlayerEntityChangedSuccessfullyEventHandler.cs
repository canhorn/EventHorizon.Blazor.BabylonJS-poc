namespace EventHorizon.Game.Client.Systems.Player.Changed;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Systems.Entity.Changed;
using EventHorizon.Game.Client.Systems.Player.Api;

using MediatR;

public class CheckForPlayerEntityChangedSuccessfullyEventHandler
    : INotificationHandler<EntityChangedSuccessfullyEvent>
{
    private readonly IMediator _mediator;
    private readonly IPlayerState _state;

    public CheckForPlayerEntityChangedSuccessfullyEventHandler(
        IMediator mediator,
        IPlayerState state
    )
    {
        _mediator = mediator;
        _state = state;
    }

    public async Task Handle(
        EntityChangedSuccessfullyEvent notification,
        CancellationToken cancellationToken
    )
    {
        if (
            _state.Player.HasValue
            && notification.EntityId == _state.Player.Value.EntityId
        )
        {
            await _mediator.Publish(
                new PlayerDetailsChangedEvent(),
                cancellationToken
            );
        }
    }
}
