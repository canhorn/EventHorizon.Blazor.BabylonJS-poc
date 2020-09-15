namespace EventHorizon.Game.Client.Systems.ClientAssets.Config.Register
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    public struct ClientAssetConfigTypeRegistered
        : INotification
    {

    }

    public interface ClientAssetConfigTypeRegisteredObserver
        : ArgumentObserver<ClientAssetConfigTypeRegistered>
    {
    }

    public class ClientAssetConfigTypeRegisteredObserverHandler
        : INotificationHandler<ClientAssetConfigTypeRegistered>
    {
        private readonly ObserverState _observer;

        public ClientAssetConfigTypeRegisteredObserverHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            ClientAssetConfigTypeRegistered notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<ClientAssetConfigTypeRegisteredObserver, ClientAssetConfigTypeRegistered>(
            notification,
            cancellationToken
        );
    }
}