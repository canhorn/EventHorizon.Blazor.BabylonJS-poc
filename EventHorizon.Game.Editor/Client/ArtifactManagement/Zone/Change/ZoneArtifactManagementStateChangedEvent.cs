namespace EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Change;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

public struct ZoneArtifactManagementStateChangedEvent : INotification { }

public interface ZoneArtifactManagementStateChangedEventObserver
    : ArgumentObserver<ZoneArtifactManagementStateChangedEvent> { }

public class ZoneArtifactManagementStateChangedEventObserverHandler
    : INotificationHandler<ZoneArtifactManagementStateChangedEvent>
{
    private readonly ObserverState _observer;

    public ZoneArtifactManagementStateChangedEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        ZoneArtifactManagementStateChangedEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            ZoneArtifactManagementStateChangedEventObserver,
            ZoneArtifactManagementStateChangedEvent
        >(notification, cancellationToken);
}
