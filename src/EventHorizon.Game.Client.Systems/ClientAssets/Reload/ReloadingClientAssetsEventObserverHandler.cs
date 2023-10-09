namespace EventHorizon.Game.Client.Systems.ClientAssets.Reload;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Observer.State;

using MediatR;

public class ReloadingClientAssetsEventObserverHandler
    : INotificationHandler<ReloadingClientAssetsEvent>
{
    private readonly ObserverState _observer;

    public ReloadingClientAssetsEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        ReloadingClientAssetsEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            ReloadingClientAssetsEventObserver,
            ReloadingClientAssetsEvent
        >(notification, cancellationToken);
}
