namespace EventHorizon.Game.Editor.Client.AssetManagement.Trigger;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.AssetManagement.Api;
using EventHorizon.Game.Editor.Client.AssetManagement.Changed;
using EventHorizon.Game.Server.Asset.Trigger;

using MediatR;

public class TriggerAssetServerBackupCommandHandler
    : IRequestHandler<TriggerAssetServerBackupCommand, StandardCommandResult>
{
    private readonly IMediator _mediator;
    private readonly AssetManagementState _state;

    public TriggerAssetServerBackupCommandHandler(
        IMediator mediator,
        AssetManagementState state
    )
    {
        _mediator = mediator;
        _state = state;
    }

    public async Task<StandardCommandResult> Handle(
        TriggerAssetServerBackupCommand request,
        CancellationToken cancellationToken
    )
    {
        var result = await _mediator.Send(
            new TriggerAssetServerAssetsBackupCommand(),
            cancellationToken
        );

        if (!result)
        {
            return new(result.ErrorCode);
        }

        _state.SetBackupReferenceId(result.Result.ReferenceId);
        await _mediator.Publish(
            new AssetManagementStateChangedEvent(),
            cancellationToken
        );

        return new();
    }
}
