namespace EventHorizon.Game.Server.Asset.Finished;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Observer.State;

using MediatR;

public class AssetServerExportUploadedEventObserverHandler
    : INotificationHandler<AssetServerExportUploadedEvent>
{
    private readonly ObserverState _observer;

    public AssetServerExportUploadedEventObserverHandler(
        ObserverState observer
    )
    {
        _observer = observer;
    }

    public Task Handle(
        AssetServerExportUploadedEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            AssetServerExportUploadedEventObserver,
            AssetServerExportUploadedEvent
        >(notification, cancellationToken);
}
