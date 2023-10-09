namespace EventHorizon.Game.Client.Systems.Player.Platform;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Lifecycle.Dispose;
using EventHorizon.Game.Client.Systems.Player.Api;

using MediatR;

public class CleanupPlayerOnDisposeOfEngineEventHandler
    : INotificationHandler<DisposeOfEngineEvent>
{
    private readonly IPlayerState _state;

    public CleanupPlayerOnDisposeOfEngineEventHandler(IPlayerState state)
    {
        _state = state;
    }

    public Task Handle(
        DisposeOfEngineEvent notification,
        CancellationToken cancellationToken
    )
    {
        _state.Reset();

        return Task.CompletedTask;
    }
}
