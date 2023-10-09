namespace EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Trigger;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Api;
using EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Change;
using EventHorizon.Zone.Systems.ArtifactManagement.Trigger;

using MediatR;

public class TriggerZoneServerArtifactBackupCommandHandler
    : IRequestHandler<
        TriggerZoneServerArtifactBackupCommand,
        StandardCommandResult
    >
{
    private readonly IMediator _mediator;
    private readonly ZoneArtifactManagementState _state;

    public TriggerZoneServerArtifactBackupCommandHandler(
        IMediator mediator,
        ZoneArtifactManagementState state
    )
    {
        _mediator = mediator;
        _state = state;
    }

    public async Task<StandardCommandResult> Handle(
        TriggerZoneServerArtifactBackupCommand request,
        CancellationToken cancellationToken
    )
    {
        var result = await _mediator.Send(
            new TriggerZoneArtifactBackupCommand(),
            cancellationToken
        );

        if (!result)
        {
            return result.ErrorCode;
        }

        _state.SetBackupReferenceId(result.Result.ReferenceId);
        await _mediator.Publish(
            new ZoneArtifactManagementStateChangedEvent(),
            cancellationToken
        );

        return new();
    }
}
