namespace EventHorizon.Game.Editor.Core.Services.Connection
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;


    public struct ConnectionUnauthorizedEvent
        : INotification
    {
        public string Identifier { get; }

        public ConnectionUnauthorizedEvent(
            string identifier = null
        )
        {
            Identifier = identifier ?? string.Empty;
        }
    }

    public interface ConnectionUnauthorizedEventObserver
        : ArgumentObserver<ConnectionUnauthorizedEvent>
    {
    }

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
