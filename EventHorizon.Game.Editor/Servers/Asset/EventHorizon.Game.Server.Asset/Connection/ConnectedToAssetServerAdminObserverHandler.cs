namespace EventHorizon.Game.Server.Asset.Connection
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Observer.State;
    using MediatR;

    public class ConnectedToAssetServerAdminObserverHandler
        : INotificationHandler<ConnectedToAssetServerAdmin>
    {
        private readonly ObserverState _observer;

        public ConnectedToAssetServerAdminObserverHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            ConnectedToAssetServerAdmin notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<ConnectedToAssetServerAdminObserver, ConnectedToAssetServerAdmin>(
            notification,
            cancellationToken
        );
    }
}
