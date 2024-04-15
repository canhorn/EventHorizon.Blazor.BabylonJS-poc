namespace EventHorizon.Game.Editor.Client.AssetManagement.Delete;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.AssetManagement.Api;
using EventHorizon.Game.Editor.Client.AssetManagement.Changed;
using MediatR;

public class AssetFileDeleteDirectoryContentCommandHandler
    : IRequestHandler<AssetFileDeleteDirectoryContentCommand, StandardCommandResult>
{
    private readonly IMediator _mediator;
    private readonly AssetManagementState _assetManagementState;

    public AssetFileDeleteDirectoryContentCommandHandler(
        IMediator mediator,
        AssetManagementState assetManagementState
    )
    {
        _mediator = mediator;
        _assetManagementState = assetManagementState;
    }

    public async Task<StandardCommandResult> Handle(
        AssetFileDeleteDirectoryContentCommand request,
        CancellationToken cancellationToken
    )
    {
        await _assetManagementState.DeleteDirectoryContent(
            request.DirectoryContent,
            cancellationToken
        );

        await _mediator.Publish(new AssetManagementStateChangedEvent(), cancellationToken);

        return new();
    }
}
