namespace EventHorizon.Game.Server.Asset.Finished;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Observer.State;

using MediatR;

public class AssetServerImportUploadedEventObserverHandler
    : INotificationHandler<AssetServerImportUploadedEvent>
{
    private readonly ObserverState _observer;

    public AssetServerImportUploadedEventObserverHandler(
        ObserverState observer
    )
    {
        _observer = observer;
    }

    public Task Handle(
        AssetServerImportUploadedEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            AssetServerImportUploadedEventObserver,
            AssetServerImportUploadedEvent
        >(notification, cancellationToken);
}
