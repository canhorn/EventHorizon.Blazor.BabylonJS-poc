namespace EventHorizon.Game.Client.Systems.Player.Action.Send
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Invoke;
    using EventHorizon.Game.Client.Systems.Player.Action.Model.Send;
    using MediatR;

    public class InvokePlayerActionEventHandler
        : INotificationHandler<InvokePlayerActionEvent>
    {
        private readonly IMediator _mediator;

        public InvokePlayerActionEventHandler(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        public Task Handle(
            InvokePlayerActionEvent notification,
            CancellationToken cancellationToken
        )
        {
            return _mediator.Send(
                new InvokeMethodOnZoneConnectionCommand(
                    "PlayerAction",
                    new List<object>
                    {
                        notification.Action,
                        notification.Data,
                    }
                )
            );
        }
    }
}
