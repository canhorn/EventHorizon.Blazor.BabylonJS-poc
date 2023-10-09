namespace EventHorizon.Game.Server.Asset.Finished;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Observer.State;

using MediatR;

public class AssetServerBackupFinishedEventObserverHandler
    : INotificationHandler<AssetServerBackupFinishedEvent>
{
    private readonly ObserverState _observer;

    public AssetServerBackupFinishedEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        AssetServerBackupFinishedEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            AssetServerBackupFinishedEventObserver,
            AssetServerBackupFinishedEvent
        >(notification, cancellationToken);
}
