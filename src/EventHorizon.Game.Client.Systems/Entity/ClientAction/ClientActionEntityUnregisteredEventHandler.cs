namespace EventHorizon.Game.Client.Systems.Entity.ClientAction;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Entity.Tag;
using EventHorizon.Game.Client.Engine.Entity.Tracking.Query;
using EventHorizon.Game.Client.Engine.Lifecycle.Register.Dispose;
using EventHorizon.Game.Client.Engine.Systems.Entity.ClientAction;
using MediatR;

public class ClientActionEntityUnregisteredEventHandler
    : INotificationHandler<ClientActionEntityUnregisteredEvent>
{
    private readonly IMediator _mediator;

    public ClientActionEntityUnregisteredEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(
        ClientActionEntityUnregisteredEvent notification,
        CancellationToken cancellationToken
    )
    {
        var entitiesResult = await _mediator.Send(
            new QueryForEntity(TagBuilder.CreateEntityIdTag(notification.EntityId.ToString())),
            cancellationToken
        );
        if (entitiesResult.Success && entitiesResult.Result.Any())
        {
            await _mediator.Send(
                new DisposeOfEntityCommand(entitiesResult.Result.First()),
                cancellationToken
            );
        }
    }
}
