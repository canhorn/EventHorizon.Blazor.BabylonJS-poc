namespace EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Trigger;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Api;
using EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Change;
using EventHorizon.Zone.Systems.ArtifactManagement.Trigger;

using MediatR;

public class TriggerZoneServerArtifactImportCommandHandler
    : IRequestHandler<
        TriggerZoneServerArtifactImportCommand,
        StandardCommandResult
    >
{
    private readonly ISender _sender;
    private readonly IPublisher _publisher;
    private readonly ZoneArtifactManagementState _state;

    public TriggerZoneServerArtifactImportCommandHandler(
        ISender sender,
        IPublisher publisher,
        ZoneArtifactManagementState state
    )
    {
        _sender = sender;
        _publisher = publisher;
        _state = state;
    }

    public async Task<StandardCommandResult> Handle(
        TriggerZoneServerArtifactImportCommand request,
        CancellationToken cancellationToken
    )
    {
        var result = await _sender.Send(
            new TriggerZoneArtifactImportCommand(request.ImportArtifactUrl),
            cancellationToken
        );

        if (!result)
        {
            return result.ErrorCode;
        }

        _state.SetImportReferenceId(result.Result.ReferenceId);
        await _publisher.Publish(
            new ZoneArtifactManagementStateChangedEvent(),
            cancellationToken
        );

        return new();
    }
}
