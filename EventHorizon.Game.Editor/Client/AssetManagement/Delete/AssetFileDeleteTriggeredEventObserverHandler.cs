namespace EventHorizon.Game.Editor.Client.AssetManagement.Delete;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Observer.State;

using MediatR;

public class AssetFileDeleteTriggeredEventObserverHandler
    : INotificationHandler<AssetFileDeleteTriggeredEvent>
{
    private readonly ObserverState _observer;

    public AssetFileDeleteTriggeredEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        AssetFileDeleteTriggeredEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            AssetFileDeleteTriggeredEventObserver,
            AssetFileDeleteTriggeredEvent
        >(notification, cancellationToken);
}
