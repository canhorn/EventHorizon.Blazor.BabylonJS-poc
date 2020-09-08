namespace EventHorizon.Game.Client.Engine.Systems.Entity.Actions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Register;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Register;
    using EventHorizon.Game.Client.Systems.Entity.Model;
    using MediatR;

    public class ClientActionEntityRegisteredEventHandler
        : INotificationHandler<ClientActionEntityRegisteredEvent>
    {
        private readonly IMediator _mediator;

        public ClientActionEntityRegisteredEventHandler(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        public async Task Handle(
            ClientActionEntityRegisteredEvent notification,
            CancellationToken cancellationToken
        )
        {
            await _mediator.Publish(
                new RegisterEntityEvent(
                    new StandardServerEntity(
                        notification.Entity
                    )
                )
            );
        }
    }
}
