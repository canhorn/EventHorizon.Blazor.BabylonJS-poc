namespace EventHorizon.Game.Editor.Zone.Systems.ArtifactManagement.Finished;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.State;
using EventHorizon.Zone.Systems.ArtifactManagement.Finished;
using MediatR;

public class ZoneServerExportFinishedEventObserverHandler
    : INotificationHandler<ZoneServerExportFinishedEvent>
{
    private readonly ObserverState _observer;

    public ZoneServerExportFinishedEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        ZoneServerExportFinishedEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<ZoneServerExportFinishedEventObserver, ZoneServerExportFinishedEvent>(
            notification,
            cancellationToken
        );
}
