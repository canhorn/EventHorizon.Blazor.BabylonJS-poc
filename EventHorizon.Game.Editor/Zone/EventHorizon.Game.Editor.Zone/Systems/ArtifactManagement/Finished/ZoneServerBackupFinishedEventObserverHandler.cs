namespace EventHorizon.Game.Editor.Zone.Systems.ArtifactManagement.Finished;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.State;
using EventHorizon.Zone.Systems.ArtifactManagement.Finished;
using MediatR;

public class ZoneServerBackupFinishedEventObserverHandler
    : INotificationHandler<ZoneServerBackupFinishedEvent>
{
    private readonly ObserverState _observer;

    public ZoneServerBackupFinishedEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        ZoneServerBackupFinishedEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<ZoneServerBackupFinishedEventObserver, ZoneServerBackupFinishedEvent>(
            notification,
            cancellationToken
        );
}
