namespace EventHorizon.Game.Editor.Connection
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Connection.Shared.Unauthorized;
    using EventHorizon.Observer.State;
    using MediatR;

    public class ConnectionUnauthorizedEventObserverHandler
        : INotificationHandler<ConnectionUnauthorizedEvent>
    {
        private readonly ObserverState _observer;

        public ConnectionUnauthorizedEventObserverHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            ConnectionUnauthorizedEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<ConnectionUnauthorizedEventObserver, ConnectionUnauthorizedEvent>(
            notification,
            cancellationToken
        );
    }
}
