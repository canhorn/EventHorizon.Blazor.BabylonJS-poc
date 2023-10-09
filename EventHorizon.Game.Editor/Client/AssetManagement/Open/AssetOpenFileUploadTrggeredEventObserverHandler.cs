namespace EventHorizon.Game.Editor.Client.AssetManagement.Open;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Observer.State;

using MediatR;

public class AssetOpenFileUploadTrggeredEventObserverHandler
    : INotificationHandler<AssetOpenFileUploadTrggeredEvent>
{
    private readonly ObserverState _observer;

    public AssetOpenFileUploadTrggeredEventObserverHandler(
        ObserverState observer
    )
    {
        _observer = observer;
    }

    public Task Handle(
        AssetOpenFileUploadTrggeredEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            AssetOpenFileUploadTrggeredEventObserver,
            AssetOpenFileUploadTrggeredEvent
        >(notification, cancellationToken);
}
