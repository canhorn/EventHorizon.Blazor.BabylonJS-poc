namespace EventHorizon.Game.Client.Systems.Entity.Instanced.Registered;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Lifecycle.Register.Register;
using EventHorizon.Game.Client.Engine.Systems.Entity.Register;
using EventHorizon.Game.Client.Systems.Entity.Instanced.Model;

using MediatR;

public class RegisterClientEntityInstancedOnRegisteredEventHandler
    : INotificationHandler<ClientEntityRegistered>
{
    private readonly IMediator _mediator;

    public RegisterClientEntityInstancedOnRegisteredEventHandler(
        IMediator mediator
    )
    {
        _mediator = mediator;
    }

    public async Task Handle(
        ClientEntityRegistered notification,
        CancellationToken cancellationToken
    )
    {
        await _mediator.Publish(
            new RegisterEntityEvent(
                new ClientEntityInstanced(notification.EntityDetails)
            ),
            cancellationToken
        );
    }
}
