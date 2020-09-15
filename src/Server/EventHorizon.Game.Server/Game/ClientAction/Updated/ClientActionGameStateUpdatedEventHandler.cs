namespace EventHorizon.Game.Server.Game.ClientAction.Updated
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Server.Game.Set;
    using MediatR;

    public class ClientActionGameStateUpdatedEventHandler
        : INotificationHandler<ClientActionGameStateUpdatedEvent>
    {
        private readonly IMediator _mediator;

        public ClientActionGameStateUpdatedEventHandler(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        public async Task Handle(
            ClientActionGameStateUpdatedEvent notification, 
            CancellationToken cancellationToken
        )
        {
            await _mediator.Send(
                new SetGameStateCommand(
                    notification.GameState
                ),
                cancellationToken
            );
        }
    }
}
