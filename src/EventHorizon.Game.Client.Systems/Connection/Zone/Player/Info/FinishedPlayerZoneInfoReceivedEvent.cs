namespace EventHorizon.Game.Client.Systems.Connection.Zone.Player.Info;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

public struct FinishedPlayerZoneInfoReceivedEvent : INotification { }

public interface FinishedPlayerZoneInfoReceivedEventObserver
    : ArgumentObserver<FinishedPlayerZoneInfoReceivedEvent> { }

public class FinishedPlayerZoneInfoReceivedEventHandler
    : INotificationHandler<FinishedPlayerZoneInfoReceivedEvent>
{
    private readonly ObserverState _observer;

    public FinishedPlayerZoneInfoReceivedEventHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        FinishedPlayerZoneInfoReceivedEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            FinishedPlayerZoneInfoReceivedEventObserver,
            FinishedPlayerZoneInfoReceivedEvent
        >(notification, cancellationToken);
}
