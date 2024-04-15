namespace EventHorizon.Game.Server.Asset.Finished;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.State;
using MediatR;

public class AssetServerBackupUploadedEventObserverHandler
    : INotificationHandler<AssetServerBackupUploadedEvent>
{
    private readonly ObserverState _observer;

    public AssetServerBackupUploadedEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        AssetServerBackupUploadedEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<AssetServerBackupUploadedEventObserver, AssetServerBackupUploadedEvent>(
            notification,
            cancellationToken
        );
}
