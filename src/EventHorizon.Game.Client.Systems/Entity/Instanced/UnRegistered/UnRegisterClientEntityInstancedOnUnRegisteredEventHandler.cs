namespace EventHorizon.Game.Client.Systems.Entity.Instanced.UnRegistered;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Entity.Tag;
using EventHorizon.Game.Client.Engine.Entity.Tracking.Query;
using EventHorizon.Game.Client.Engine.Lifecycle.Register.Dispose;
using EventHorizon.Game.Client.Engine.Systems.Entity.Unregister;

using MediatR;

using Microsoft.Extensions.Logging;

public class UnRegisterClientEntityInstancedOnUnRegisteredEventHandler
    : INotificationHandler<ClientEntityUnregisteredEvent>
{
    private readonly IMediator _mediator;

    public UnRegisterClientEntityInstancedOnUnRegisteredEventHandler(
        IMediator mediator
    )
    {
        _mediator = mediator;
    }

    public async Task Handle(
        ClientEntityUnregisteredEvent notification,
        CancellationToken cancellationToken
    )
    {
        var result = await _mediator.Send(
            new QueryForEntity(
                TagBuilder.CreateGlobalIdTag(notification.GlobalId)
            ),
            cancellationToken
        );
        GamePlatfrom
            .Logger<QueryForEntity>()
            .LogWarning(
                "Found [{ResultCount}] Entities of '{GlobalId}' GlobalId to Dispose.",
                result.Result.Count(),
                notification.GlobalId
            );

        if (result.Success && result.Result.Any())
        {
            await _mediator.Send(
                new DisposeOfEntityCommand(result.Result.First()),
                cancellationToken
            );
        }
    }
}
