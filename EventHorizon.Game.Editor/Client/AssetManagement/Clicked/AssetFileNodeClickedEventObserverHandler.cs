namespace EventHorizon.Game.Editor.Client.AssetManagement.Clicked
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Observer.State;
    using MediatR;

    public class AssetFileNodeClickedEventObserverHandler
        : INotificationHandler<AssetFileNodeClickedEvent>
    {
        private readonly ObserverState _observer;

        public AssetFileNodeClickedEventObserverHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            AssetFileNodeClickedEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<AssetFileNodeClickedEventObserver, AssetFileNodeClickedEvent>(
            notification,
            cancellationToken
        );
    }
}
