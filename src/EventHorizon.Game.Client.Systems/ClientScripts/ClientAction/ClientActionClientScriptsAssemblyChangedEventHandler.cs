namespace EventHorizon.Game.Client.Systems.ClientScripts.ClientAction
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Systems.ClientScripts.Scripting.ClientAction;
    using EventHorizon.Game.Client.Systems.ClientScripts.Set;
    using MediatR;

    public class ClientActionClientScriptsAssemblyChangedEventHandler
        : INotificationHandler<ClientActionClientScriptsAssemblyChangedEvent>
    {
        private readonly IMediator _mediator;

        public ClientActionClientScriptsAssemblyChangedEventHandler(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        public async Task Handle(
            ClientActionClientScriptsAssemblyChangedEvent notification,
            CancellationToken cancellationToken
        )
        {
            await _mediator.Send(
                new SetClientScriptsAssemblyCommand(
                    notification.Hash,
                    notification.ScriptAssembly
                ),
                cancellationToken
            );
        }
    }
}
