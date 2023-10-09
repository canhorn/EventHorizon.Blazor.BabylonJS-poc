namespace EventHorizon.Game.Client.Systems.Zone.Changed;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;

using MediatR;

public struct ZoneChangedEvent : INotification { }

public interface ZoneChangedEventObserver
    : ArgumentObserver<ZoneChangedEvent> { }

public class ZoneChangedEventHandler : INotificationHandler<ZoneChangedEvent>
{
    private readonly ObserverState _observer;

    public ZoneChangedEventHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        ZoneChangedEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<ZoneChangedEventObserver, ZoneChangedEvent>(
            notification,
            cancellationToken
        );
}
