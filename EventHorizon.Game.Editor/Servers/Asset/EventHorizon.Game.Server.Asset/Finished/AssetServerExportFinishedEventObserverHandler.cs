namespace EventHorizon.Game.Server.Asset.Finished;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.State;
using MediatR;

public class AssetServerExportFinishedEventObserverHandler
    : INotificationHandler<AssetServerExportFinishedEvent>
{
    private readonly ObserverState _observer;

    public AssetServerExportFinishedEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        AssetServerExportFinishedEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<AssetServerExportFinishedEventObserver, AssetServerExportFinishedEvent>(
            notification,
            cancellationToken
        );
}
