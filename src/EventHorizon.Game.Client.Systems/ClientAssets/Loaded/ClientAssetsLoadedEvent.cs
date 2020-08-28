namespace EventHorizon.Game.Client.Systems.ClientAssets.Loaded
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    public class ClientAssetsLoadedEvent
        : INotification
    {
    }

    public interface ClientAssetsLoadedEventObserver
        : ArgumentObserver<ClientAssetsLoadedEvent>
    {
    }

    public class ClientAssetsLoadedEventHandler
        : INotificationHandler<ClientAssetsLoadedEvent>
    {
        private readonly ObserverState _observer;

        public ClientAssetsLoadedEventHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            ClientAssetsLoadedEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<ClientAssetsLoadedEventObserver, ClientAssetsLoadedEvent>(
            notification,
            cancellationToken
        );
    }
}
