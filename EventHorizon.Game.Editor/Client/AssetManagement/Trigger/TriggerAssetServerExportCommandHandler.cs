namespace EventHorizon.Game.Editor.Client.AssetManagement.Trigger;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.AssetManagement.Api;
using EventHorizon.Game.Editor.Client.AssetManagement.Changed;
using EventHorizon.Game.Server.Asset.Trigger;

using MediatR;

public class TriggerAssetServerExportCommandHandler
    : IRequestHandler<TriggerAssetServerExportCommand, StandardCommandResult>
{
    private readonly IMediator _mediator;
    private readonly AssetManagementState _state;

    public TriggerAssetServerExportCommandHandler(
        IMediator mediator,
        AssetManagementState state
    )
    {
        _mediator = mediator;
        _state = state;
    }

    public async Task<StandardCommandResult> Handle(
        TriggerAssetServerExportCommand request,
        CancellationToken cancellationToken
    )
    {
        var result = await _mediator.Send(
            new TriggerAssetServerAssetsExportCommand(),
            cancellationToken
        );

        if (!result)
        {
            return new(result.ErrorCode);
        }

        _state.SetExportReferenceId(
            result.Result.ReferenceId
        );
        await _mediator.Publish(
            new AssetManagementStateChangedEvent(),
            cancellationToken
        );

        return new();
    }
}
