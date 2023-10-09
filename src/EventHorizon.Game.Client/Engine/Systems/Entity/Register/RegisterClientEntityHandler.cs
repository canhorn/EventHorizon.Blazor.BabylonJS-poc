namespace EventHorizon.Game.Client.Engine.Systems.Entity.Register;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;

using MediatR;

public class RegisterClientEntityHandler
    : IRequestHandler<RegisterClientEntity, StandardCommandResult>
{
    private readonly IMediator _mediator;
    private readonly IEntityDetailsState _state;

    public RegisterClientEntityHandler(
        IMediator mediator,
        IEntityDetailsState state
    )
    {
        _mediator = mediator;
        _state = state;
    }

    public async Task<StandardCommandResult> Handle(
        RegisterClientEntity request,
        CancellationToken cancellationToken
    )
    {
        await _mediator.Publish(
            new RegisteringClientEntity(request.EntityDetails),
            cancellationToken
        );
        _state.Set(request.EntityDetails);
        await _mediator.Publish(
            new ClientEntityRegistered(request.EntityDetails),
            cancellationToken
        );

        return new StandardCommandResult();
    }
}
