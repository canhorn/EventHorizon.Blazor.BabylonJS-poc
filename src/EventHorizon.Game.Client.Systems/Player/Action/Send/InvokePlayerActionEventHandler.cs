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
        private static IDictionary<string, object> EMPTY_DATA => new Dictionary<string, object>();
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
            var data = new List<object>
            {
                notification.Action,
            };
            if (notification.Data.IsNotNull())
            {
                data.Add(
                    notification.Data
                );
            }
            else
            {
                data.Add(
                    EMPTY_DATA
                );
            }

            return _mediator.Send(
                new InvokeMethodOnZoneConnectionCommand(
                    "PlayerAction",
                    data
                ),
                cancellationToken
            );
        }
    }
}
