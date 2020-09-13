﻿namespace EventHorizon.Game.Client.Systems.ServerModule.Reload
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Gui.Reload;
    using EventHorizon.Game.Client.Systems.ServerModule.Register;
    using EventHorizon.Game.Client.Systems.ServerModule.Api;
    using EventHorizon.Game.Client.Systems.ServerModule.Dispose;
    using MediatR;

    public class GuiSystemFinishedReloadingRebuildServerModuleSystemHandler
        : INotificationHandler<GuiSystemFinishedReloadingEvent>
    {
        private readonly IMediator _mediator;
        private readonly ServerModuleState _serverModuleState;
        private readonly ServerModuleScriptsState _serverModuleScriptsState;

        public GuiSystemFinishedReloadingRebuildServerModuleSystemHandler(
            IMediator mediator,
            ServerModuleState serverModuleState,
            ServerModuleScriptsState serverModuleScriptsState
        )
        {
            _mediator = mediator;
            _serverModuleState = serverModuleState;
            _serverModuleScriptsState = serverModuleScriptsState;
        }

        public async Task Handle(
            GuiSystemFinishedReloadingEvent notification, 
            CancellationToken cancellationToken
        )
        {
            // Remove all existing Server Modules
            foreach (var serverModule in _serverModuleState.All())
            {
                await _mediator.Send(
                    new DisposeOfServerModuleCommand(
                        serverModule.Name
                    ),
                    cancellationToken
                );
            }

            // Create all new Server Modules from Existing Scripts
            foreach (var serverModuleScripts in _serverModuleScriptsState.All())
            {
                await _mediator.Send(
                    new RegisterNewServerModuleFromScriptCommand(
                        serverModuleScripts
                    ),
                    cancellationToken
                );
            }
        }
    }
}
