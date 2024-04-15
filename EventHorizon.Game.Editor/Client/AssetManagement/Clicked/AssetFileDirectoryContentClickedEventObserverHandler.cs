namespace EventHorizon.Game.Editor.Client.AssetManagement.Clicked;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.State;
using MediatR;

public class AssetFileDirectoryContentClickedEventObserverHandler
    : INotificationHandler<AssetFileDirectoryContentClickedEvent>
{
    private readonly ObserverState _observer;

    public AssetFileDirectoryContentClickedEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        AssetFileDirectoryContentClickedEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            AssetFileDirectoryContentClickedEventObserver,
            AssetFileDirectoryContentClickedEvent
        >(notification, cancellationToken);
}
