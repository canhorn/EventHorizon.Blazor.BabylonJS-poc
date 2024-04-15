namespace EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Trigger;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Triggered;
using EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Upload;
using MediatR;

public class TriggerImportOnSuccessfullyUploadedZoneServerImportFileEventHandler
    : INotificationHandler<SuccessfullyUploadedZoneServerImportFileEvent>
{
    private readonly ISender _sender;
    private readonly IPublisher _publisher;

    public TriggerImportOnSuccessfullyUploadedZoneServerImportFileEventHandler(
        ISender sender,
        IPublisher publisher
    )
    {
        _sender = sender;
        _publisher = publisher;
    }

    public async Task Handle(
        SuccessfullyUploadedZoneServerImportFileEvent notification,
        CancellationToken cancellationToken
    )
    {
        var result = await _sender.Send(
            new TriggerZoneServerArtifactImportCommand(notification.Url),
            cancellationToken
        );

        await _publisher.Publish(
            new TriggredZoneServerArtifactImportEvent(result.Success, result.ErrorCode),
            cancellationToken
        );
    }
}
