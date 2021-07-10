namespace EventHorizon.Game.Editor.Client.AssetManagement.Changed
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Observer.State;
    using MediatR;

    public class AssetManagementStateChangedEventObserverHandler
        : INotificationHandler<AssetManagementStateChangedEvent>
    {
        private readonly ObserverState _observer;

        public AssetManagementStateChangedEventObserverHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
             AssetManagementStateChangedEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<AssetManagementStateChangedEventObserver, AssetManagementStateChangedEvent>(
            notification,
            cancellationToken
        );
    }
}
