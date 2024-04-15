namespace EventHorizon.Game.Client.Systems.Connection.Zone.Player.Info;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Api;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

public struct PlayerZoneInfoReceivedEvent : INotification
{
    public IPlayerZoneInfo PlayerZoneInfo { get; }

    public PlayerZoneInfoReceivedEvent(IPlayerZoneInfo playerZoneInfo)
    {
        PlayerZoneInfo = playerZoneInfo;
    }
}

public interface PlayerZoneInfoReceivedEventObserver
    : ArgumentObserver<PlayerZoneInfoReceivedEvent> { }

public class PlayerZoneInfoReceivedEventHandler : INotificationHandler<PlayerZoneInfoReceivedEvent>
{
    private readonly ObserverState _observer;

    public PlayerZoneInfoReceivedEventHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        PlayerZoneInfoReceivedEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<PlayerZoneInfoReceivedEventObserver, PlayerZoneInfoReceivedEvent>(
            notification,
            cancellationToken
        );
}
