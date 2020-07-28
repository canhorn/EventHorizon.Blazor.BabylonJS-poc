namespace EventHorizon.Game.Client.Systems.Connection.Zone.Player.Info
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    public struct FinishedZonePlayerInfoReceivedEvent 
        : INotification
    {

    }

    public interface FinishedZonePlayerInfoReceivedEventObserver
        : ArgumentObserver<FinishedZonePlayerInfoReceivedEvent>
    {
    }

    public class FinishedZonePlayerInfoReceivedEventHandler
        : INotificationHandler<FinishedZonePlayerInfoReceivedEvent>
    {
        private readonly ObserverState _observer;

        public FinishedZonePlayerInfoReceivedEventHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            FinishedZonePlayerInfoReceivedEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<FinishedZonePlayerInfoReceivedEventObserver, FinishedZonePlayerInfoReceivedEvent>(
            notification,
            cancellationToken
        );
    }
}
