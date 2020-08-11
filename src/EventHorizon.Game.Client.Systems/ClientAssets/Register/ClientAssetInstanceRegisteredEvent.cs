namespace EventHorizon.Game.Client.Systems.ClientAssets.Register
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    public struct ClientAssetInstanceRegisteredEvent
        : INotification
    {
        public IClientAssetInstance ClientAssetInstance { get; }

        public ClientAssetInstanceRegisteredEvent(
            IClientAssetInstance clientAssetInstance
        )
        {
            ClientAssetInstance = clientAssetInstance;
        }
    }

    public interface ClientAssetInstanceRegisteredEventObserver
        : ArgumentObserver<ClientAssetInstanceRegisteredEvent>
    {
    }

    public class ClientAssetInstanceRegisteredEventHandler
        : INotificationHandler<ClientAssetInstanceRegisteredEvent>
    {
        private readonly ObserverState _observer;

        public ClientAssetInstanceRegisteredEventHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            ClientAssetInstanceRegisteredEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<ClientAssetInstanceRegisteredEventObserver, ClientAssetInstanceRegisteredEvent>(
            notification,
            cancellationToken
        );
    }
}
