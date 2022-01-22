namespace EventHorizon.Game.Client.Systems.ClientAssets.Register;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Systems.ClientAssets.Api;
using EventHorizon.Game.Client.Systems.ClientAssets.Dispose;

using MediatR;

public class RegisterClientAssetInstanceCommandHandler
    : IRequestHandler<RegisterClientAssetInstanceCommand, StandardCommandResult>
{
    private readonly IMediator _mediator;
    private readonly ClientAssetInstanceState _state;

    public RegisterClientAssetInstanceCommandHandler(
        IMediator mediator,
        ClientAssetInstanceState state
    )
    {
        _mediator = mediator;
        _state = state;
    }

    public async Task<StandardCommandResult> Handle(
        RegisterClientAssetInstanceCommand request,
        CancellationToken cancellationToken
    )
    {
        await _mediator.Send(
            new DisposeOfClientAssetInstanceCommand(
                request.Instance.AssetInstanceId
            ),
            cancellationToken
        );

        _state.Set(request.Instance);
        await _mediator.Publish(
            new ClientAssetInstanceRegisteredEvent(request.Instance),
            cancellationToken
        );

        return new StandardCommandResult();
    }
}
