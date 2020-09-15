namespace EventHorizon.Game.Client.Systems.ClientScripts.ClientAction
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Systems.ClientScripts.Api;
    using EventHorizon.Game.Client.Systems.ClientScripts.Scripting.ClientAction;
    using EventHorizon.Game.Client.Systems.ClientScripts.Set;
    using EventHorizon.Game.Server.ServerModule.BackToMenu.Reload;
    using MediatR;
    using Newtonsoft.Json.Serialization;

    public class ClientActionClientScriptsAssemblyChangedEventHandler
        : INotificationHandler<ClientActionClientScriptsAssemblyChangedEvent>
    {
        private readonly IMediator _mediator;
        private readonly ClientScriptsState _state;

        public ClientActionClientScriptsAssemblyChangedEventHandler(
            IMediator mediator,
            ClientScriptsState state
        )
        {
            _mediator = mediator;
            _state = state;
        }

        public async Task Handle(
            ClientActionClientScriptsAssemblyChangedEvent notification,
            CancellationToken cancellationToken
        )
        {
            if (notification.Hash != _state.Hash)
            {
                await _mediator.Send(
                    new TriggerPageReloadCommand(),
                    cancellationToken
                );
            }
            // TODO: [SCRIPTING] - Look at how to support live reloading of scripts.
            //await _mediator.Send(
            //    new SetClientScriptsAssemblyCommand(
            //        notification.Hash,
            //        notification.ScriptAssembly
            //    ),
            //    cancellationToken
            //);
        }
    }
}
