namespace EventHorizon.Game.Editor.Client.AssetManagement.Reload;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.AssetManagement.Api;
using EventHorizon.Game.Editor.Client.AssetManagement.Changed;

using MediatR;

public class AssetReloadToNodeAndDirectoryContentCommandHandler
    : IRequestHandler<
        AssetReloadToNodeAndDirectoryContentCommand,
        StandardCommandResult
    >
{
    private readonly IMediator _mediator;
    private readonly AssetManagementState _state;

    public AssetReloadToNodeAndDirectoryContentCommandHandler(
        IMediator mediator,
        AssetManagementState state
    )
    {
        _mediator = mediator;
        _state = state;
    }

    public async Task<StandardCommandResult> Handle(
        AssetReloadToNodeAndDirectoryContentCommand request,
        CancellationToken cancellationToken
    )
    {
        await _state.ReloadToNodeAndDirectoryContent(
            request.Node,
            request.DirectoryContent,
            cancellationToken
        );

        await _mediator.Publish(
            new AssetManagementStateChangedEvent(),
            cancellationToken
        );

        return new();
    }
}
