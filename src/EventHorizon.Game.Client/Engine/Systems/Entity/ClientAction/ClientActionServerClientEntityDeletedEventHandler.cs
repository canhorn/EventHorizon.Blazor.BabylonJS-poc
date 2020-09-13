namespace EventHorizon.Game.Client.Engine.Systems.Entity.ClientAction
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Unregister;
    using MediatR;

    public class ClientActionServerClientEntityDeletedEventHandler
        : INotificationHandler<ClientActionServerClientEntityDeletedEvent>
    {
        private readonly IMediator _mediator;

        public ClientActionServerClientEntityDeletedEventHandler(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        public async Task Handle(
            ClientActionServerClientEntityDeletedEvent notification, 
            CancellationToken cancellationToken
        )
        {
            // UnRegister Client Entity
            await _mediator.Send(
                new UnregisterClientEntityCommand(
                    notification.GlobalId
                ),
                cancellationToken
            );
        }
    }
}
