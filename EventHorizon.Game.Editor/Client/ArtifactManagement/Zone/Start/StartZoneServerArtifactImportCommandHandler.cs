namespace EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Start;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Open;

using MediatR;

public class StartZoneServerArtifactImportCommandHandler
    : IRequestHandler<
        StartZoneServerArtifactImportCommand,
        StandardCommandResult
    >
{
    private readonly IPublisher _publisher;

    public StartZoneServerArtifactImportCommandHandler(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task<StandardCommandResult> Handle(
        StartZoneServerArtifactImportCommand request,
        CancellationToken cancellationToken
    )
    {
        await _publisher.Publish(
            new OpenZoneServerImportFileUploaderEvent(),
            cancellationToken
        );

        return new();
    }
}
