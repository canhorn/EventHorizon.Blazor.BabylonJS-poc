namespace EventHorizon.Game.Client.Systems.ServerModule.Reload;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Systems.ServerModule.Api;
using EventHorizon.Game.Client.Systems.ServerModule.ClientAction;
using EventHorizon.Game.Client.Systems.ServerModule.Dispose;
using EventHorizon.Game.Client.Systems.ServerModule.Register;
using MediatR;
using Microsoft.Extensions.Logging;

public class ClientActionServerModuleSystemReloadedEventHandler
    : INotificationHandler<ClientActionServerModuleSystemReloadedEvent>
{
    private readonly ILogger _logger;
    private readonly IMediator _mediator;
    private readonly ServerModuleState _state;
    private readonly ServerModuleScriptsState _scriptsState;

    public ClientActionServerModuleSystemReloadedEventHandler(
        ILogger<ClientActionServerModuleSystemReloadedEventHandler> logger,
        IMediator mediator,
        ServerModuleState state,
        ServerModuleScriptsState scriptsState
    )
    {
        _logger = logger;
        _mediator = mediator;
        _state = state;
        _scriptsState = scriptsState;
    }

    public async Task Handle(
        ClientActionServerModuleSystemReloadedEvent notification,
        CancellationToken cancellationToken
    )
    {
        // Dispose of existing
        foreach (var serverModule in _state.All())
        {
            await _mediator.Send(
                new DisposeOfServerModuleCommand(serverModule.Name),
                cancellationToken
            );
        }

        // Clear current state
        _state.Clear();

        // Clear out any existing Scripts
        _scriptsState.Clear();

        // Register new ServerModule
        foreach (var newServerModuleScripts in notification.ServerModuleScriptsList)
        {
            var result = await _mediator.Send(
                new RegisterNewServerModuleFromScriptCommand(newServerModuleScripts),
                cancellationToken
            );
            if (!result.Success)
            {
                _logger.LogWarning(
                    "Failed to Register ServerModule from Scripts: {ScriptsName} | {ErrorCode}",
                    newServerModuleScripts.Name,
                    result.ErrorCode
                );
            }
        }
    }
}
