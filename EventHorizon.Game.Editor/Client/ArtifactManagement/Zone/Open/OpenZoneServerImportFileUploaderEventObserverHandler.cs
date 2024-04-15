namespace EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Open;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.State;
using MediatR;

public class OpenZoneServerImportFileUploaderEventObserverHandler
    : INotificationHandler<OpenZoneServerImportFileUploaderEvent>
{
    private readonly ObserverState _observer;

    public OpenZoneServerImportFileUploaderEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        OpenZoneServerImportFileUploaderEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            OpenZoneServerImportFileUploaderEventObserver,
            OpenZoneServerImportFileUploaderEvent
        >(notification, cancellationToken);
}
