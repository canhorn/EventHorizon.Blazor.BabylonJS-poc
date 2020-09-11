namespace EventHorizon.Game.Server.ServerModule.SystemLog.ClientAction.Message
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Server.ServerModule.SystemLog.Message;
    using EventHorizon.Observer.State;
    using MediatR;

    public class ClientActionMessageFromSystemEventObserverHandler
        : INotificationHandler<ClientActionMessageFromSystemEvent>
    {
        private readonly ObserverState _observer;

        public ClientActionMessageFromSystemEventObserverHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            ClientActionMessageFromSystemEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<ClientActionMessageFromSystemEventObserver, ClientActionMessageFromSystemEvent>(
            notification,
            cancellationToken
        );
    }
}
