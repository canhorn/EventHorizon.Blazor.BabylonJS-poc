namespace EventHorizon.Game.Client.Engine.Systems.Entity.Unregister;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Systems.Entity.Api;

using MediatR;

public class UnregisterClientEntityCommandHandler
    : IRequestHandler<UnregisterClientEntityCommand, bool>
{
    private readonly IMediator _mediator;
    private readonly IEntityDetailsState _state;

    public UnregisterClientEntityCommandHandler(
        IMediator mediator,
        IEntityDetailsState state
    )
    {
        _mediator = mediator;
        _state = state;
    }

    public async Task<bool> Handle(
        UnregisterClientEntityCommand request,
        CancellationToken cancellationToken
    )
    {
        if (request.GlobalId.IsNull() || !_state.Contains(request.GlobalId))
        {
            return true;
        }

        await _mediator.Publish(
            new UnregisteringClientEntityEvent(request.GlobalId),
            cancellationToken
        );
        _state.Remove(request.GlobalId);
        await _mediator.Publish(
            new ClientEntityUnregisteredEvent(request.GlobalId),
            cancellationToken
        );

        return true;
    }
}
