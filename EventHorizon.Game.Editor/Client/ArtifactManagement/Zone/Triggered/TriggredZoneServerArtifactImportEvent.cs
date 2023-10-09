namespace EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Triggered;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;

using MediatR;

public record TriggredZoneServerArtifactImportEvent(
    bool Success,
    string ErrorCode
) : INotification;

public interface TriggredZoneServerArtifactImportEventObserver
    : ArgumentObserver<TriggredZoneServerArtifactImportEvent> { }

public class TriggredZoneServerArtifactImportEventObserverHandler
    : INotificationHandler<TriggredZoneServerArtifactImportEvent>
{
    private readonly ObserverState _observer;

    public TriggredZoneServerArtifactImportEventObserverHandler(
        ObserverState observer
    )
    {
        _observer = observer;
    }

    public Task Handle(
        TriggredZoneServerArtifactImportEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            TriggredZoneServerArtifactImportEventObserver,
            TriggredZoneServerArtifactImportEvent
        >(notification, cancellationToken);
}
