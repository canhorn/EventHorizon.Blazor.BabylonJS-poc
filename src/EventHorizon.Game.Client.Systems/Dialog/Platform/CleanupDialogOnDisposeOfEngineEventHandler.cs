namespace EventHorizon.Game.Client.Systems.Dialog.Platform;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Lifecycle.Dispose;
using EventHorizon.Game.Client.Systems.Dialog.Api;

using MediatR;

public class CleanupDialogOnDisposeOfEngineEventHandler
    : INotificationHandler<DisposeOfEngineEvent>
{
    private readonly DialogState _state;

    public CleanupDialogOnDisposeOfEngineEventHandler(DialogState state)
    {
        _state = state;
    }

    public Task Handle(
        DisposeOfEngineEvent notification,
        CancellationToken cancellationToken
    )
    {
        _state.Clear();

        return Task.CompletedTask;
    }
}
