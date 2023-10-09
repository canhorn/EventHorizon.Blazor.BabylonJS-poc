namespace EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Trigger;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Api;
using EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Change;
using EventHorizon.Zone.Systems.ArtifactManagement.Trigger;

using MediatR;

public class TriggerZoneServerArtifactExportCommandHandler
    : IRequestHandler<
        TriggerZoneServerArtifactExportCommand,
        StandardCommandResult
    >
{
    private readonly IMediator _mediator;
    private readonly ZoneArtifactManagementState _state;

    public TriggerZoneServerArtifactExportCommandHandler(
        IMediator mediator,
        ZoneArtifactManagementState state
    )
    {
        _mediator = mediator;
        _state = state;
    }

    public async Task<StandardCommandResult> Handle(
        TriggerZoneServerArtifactExportCommand request,
        CancellationToken cancellationToken
    )
    {
        var result = await _mediator.Send(
            new TriggerZoneArtifactExportCommand(),
            cancellationToken
        );

        if (!result)
        {
            return new(result.ErrorCode);
        }

        _state.SetExportReferenceId(result.Result.ReferenceId);
        await _mediator.Publish(
            new ZoneArtifactManagementStateChangedEvent(),
            cancellationToken
        );

        return new();
    }
}
