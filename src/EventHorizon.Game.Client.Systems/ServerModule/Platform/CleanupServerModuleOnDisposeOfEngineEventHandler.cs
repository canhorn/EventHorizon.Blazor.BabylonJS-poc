namespace EventHorizon.Game.Client.Systems.ServerModule.Platform;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Lifecycle.Dispose;
using EventHorizon.Game.Client.Systems.ServerModule.Api;

using MediatR;

public class CleanupServerModuleOnDisposeOfEngineEventHandler
    : INotificationHandler<DisposeOfEngineEvent>
{
    private readonly ServerModuleState _serverModuleState;
    private readonly ServerModuleScriptsState _serverModuleScriptsState;

    public CleanupServerModuleOnDisposeOfEngineEventHandler(
        ServerModuleState serverModuleState,
        ServerModuleScriptsState serverModuleScriptsState
    )
    {
        _serverModuleState = serverModuleState;
        _serverModuleScriptsState = serverModuleScriptsState;
    }

    public Task Handle(
        DisposeOfEngineEvent notification,
        CancellationToken cancellationToken
    )
    {
        _serverModuleScriptsState.Clear();
        _serverModuleState.Clear();

        return Task.CompletedTask;
    }
}
