namespace EventHorizon.Game.Editor.Zone.Services.Connection;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

public struct ZoneAdminServiceReconnectingEvent : INotification
{
    public string ZoneId { get; }

    public ZoneAdminServiceReconnectingEvent(string zoneId)
    {
        ZoneId = zoneId;
    }
}

public interface ZoneAdminServiceReconnectingEventObserver
    : ArgumentObserver<ZoneAdminServiceReconnectingEvent> { }

public class ZoneAdminServiceReconnectingEventObserverHandler
    : INotificationHandler<ZoneAdminServiceReconnectingEvent>
{
    private readonly ObserverState _observer;

    public ZoneAdminServiceReconnectingEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        ZoneAdminServiceReconnectingEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            ZoneAdminServiceReconnectingEventObserver,
            ZoneAdminServiceReconnectingEvent
        >(notification, cancellationToken);
}
