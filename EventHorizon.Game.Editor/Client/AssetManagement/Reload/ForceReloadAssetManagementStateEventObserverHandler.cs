namespace EventHorizon.Game.Editor.Client.AssetManagement.Reload;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.State;
using MediatR;

public class ForceReloadAssetManagementStateEventObserverHandler
    : INotificationHandler<ForceReloadAssetManagementStateEvent>
{
    private readonly ObserverState _observer;

    public ForceReloadAssetManagementStateEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        ForceReloadAssetManagementStateEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            ForceReloadAssetManagementStateEventObserver,
            ForceReloadAssetManagementStateEvent
        >(notification, cancellationToken);
}
