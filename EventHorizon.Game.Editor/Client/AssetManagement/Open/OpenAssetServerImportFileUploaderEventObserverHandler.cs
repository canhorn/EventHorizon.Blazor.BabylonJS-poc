namespace EventHorizon.Game.Editor.Client.AssetManagement.Open;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.State;
using MediatR;

public class OpenAssetServerImportFileUploaderEventObserverHandler
    : INotificationHandler<OpenAssetServerImportFileUploaderEvent>
{
    private readonly ObserverState _observer;

    public OpenAssetServerImportFileUploaderEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        OpenAssetServerImportFileUploaderEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            OpenAssetServerImportFileUploaderEventObserver,
            OpenAssetServerImportFileUploaderEvent
        >(notification, cancellationToken);
}
