namespace EventHorizon.Game.Client.Systems.EntityModule.Platform;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Lifecycle.Dispose;
using EventHorizon.Game.Client.Systems.EntityModule.Api;
using MediatR;

public class ResetEntityModuleOnDisposeOfEngineEventHandler
    : INotificationHandler<DisposeOfEngineEvent>
{
    private readonly EntityBaseScriptModuleState _baseScriptModuleState;
    private readonly EntityPlayerScriptModuleState _playerScriptModuleState;

    public ResetEntityModuleOnDisposeOfEngineEventHandler(
        EntityBaseScriptModuleState baseScriptModuleState,
        EntityPlayerScriptModuleState playerScriptModuleState
    )
    {
        _baseScriptModuleState = baseScriptModuleState;
        _playerScriptModuleState = playerScriptModuleState;
    }

    public Task Handle(DisposeOfEngineEvent notification, CancellationToken cancellationToken)
    {
        _baseScriptModuleState.Reset();
        _playerScriptModuleState.Reset();

        return Task.CompletedTask;
    }
}
