namespace EventHorizon.Game.Client.Systems.Player.Changed;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Observer.State;

using MediatR;

public class PlayerDetailsChangedEventHandler
    : INotificationHandler<PlayerDetailsChangedEvent>
{
    private readonly ObserverState _observer;

    public PlayerDetailsChangedEventHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        PlayerDetailsChangedEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            PlayerDetailsChangedEventObserver,
            PlayerDetailsChangedEvent
        >(notification, cancellationToken);
}
