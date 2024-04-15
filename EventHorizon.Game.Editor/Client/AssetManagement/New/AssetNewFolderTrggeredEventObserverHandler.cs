namespace EventHorizon.Game.Editor.Client.AssetManagement.New;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.State;
using MediatR;

public class AssetNewFolderTrggeredEventObserverHandler
    : INotificationHandler<AssetNewFolderTrggeredEvent>
{
    private readonly ObserverState _observer;

    public AssetNewFolderTrggeredEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        AssetNewFolderTrggeredEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<AssetNewFolderTrggeredEventObserver, AssetNewFolderTrggeredEvent>(
            notification,
            cancellationToken
        );
}
