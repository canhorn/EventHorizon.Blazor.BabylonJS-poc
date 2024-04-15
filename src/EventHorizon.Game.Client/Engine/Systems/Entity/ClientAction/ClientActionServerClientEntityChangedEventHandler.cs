namespace EventHorizon.Game.Client.Engine.Systems.Entity.ClientAction;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Systems.Entity.Register;
using EventHorizon.Game.Client.Engine.Systems.Entity.Unregister;
using MediatR;

public class ClientActionServerClientEntityChangedEventHandler
    : INotificationHandler<ClientActionServerClientEntityChangedEvent>
{
    private readonly IMediator _mediator;

    public ClientActionServerClientEntityChangedEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(
        ClientActionServerClientEntityChangedEvent notification,
        CancellationToken cancellationToken
    )
    {
        // UnRegister Client Entity
        await _mediator.Send(
            new UnregisterClientEntityCommand(notification.Details.GlobalId),
            cancellationToken
        );
        // Register Client Entity
        await _mediator.Send(new RegisterClientEntity(notification.Details), cancellationToken);
    }
}
