namespace EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Upload;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;

using MediatR;

public record SuccessfullyUploadedZoneServerImportFileEvent(
    string Url
) : INotification;

public interface SuccessfullyUploadedZoneServerImportFileEventObserver
    : ArgumentObserver<SuccessfullyUploadedZoneServerImportFileEvent>
{
}

public class SuccessfullyUploadedZoneServerImportFileEventObserverHandler
    : INotificationHandler<SuccessfullyUploadedZoneServerImportFileEvent>
{
    private readonly ObserverState _observer;

    public SuccessfullyUploadedZoneServerImportFileEventObserverHandler(
        ObserverState observer
    )
    {
        _observer = observer;
    }

    public Task Handle(
        SuccessfullyUploadedZoneServerImportFileEvent notification,
        CancellationToken cancellationToken
    ) => _observer.Trigger<SuccessfullyUploadedZoneServerImportFileEventObserver, SuccessfullyUploadedZoneServerImportFileEvent>(
        notification,
        cancellationToken
    );
}
