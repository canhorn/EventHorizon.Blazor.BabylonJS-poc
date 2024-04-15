namespace EventHorizon.Game.Editor.Client.AssetManagement.Load;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.AssetManagement.Api;
using EventHorizon.Game.Editor.Client.AssetManagement.Changed;
using MediatR;

public class LoadAssetServerFilterPathCommandHandler
    : IRequestHandler<LoadAssetServerFilterPathCommand, StandardCommandResult>
{
    private readonly IMediator _mediator;
    private readonly AssetManagementState _state;

    public LoadAssetServerFilterPathCommandHandler(IMediator mediator, AssetManagementState state)
    {
        _mediator = mediator;
        _state = state;
    }

    public async Task<StandardCommandResult> Handle(
        LoadAssetServerFilterPathCommand request,
        CancellationToken cancellationToken
    )
    {
        await _state.LoadFilterPath(request.FilterPath, cancellationToken);

        await _mediator.Publish(new AssetManagementStateChangedEvent(), cancellationToken);

        return new();
    }
}
