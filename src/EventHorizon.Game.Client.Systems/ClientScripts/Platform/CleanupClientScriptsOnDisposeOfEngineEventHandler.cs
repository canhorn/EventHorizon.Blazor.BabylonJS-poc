namespace EventHorizon.Game.Client.Systems.ClientScripts.Platform;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Lifecycle.Dispose;
using EventHorizon.Game.Client.Systems.ClientScripts.Api;

using MediatR;

public class CleanupClientScriptsOnDisposeOfEngineEventHandler
    : INotificationHandler<DisposeOfEngineEvent>
{
    private readonly ClientScriptsState _state;

    public CleanupClientScriptsOnDisposeOfEngineEventHandler(
        ClientScriptsState state
    )
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
