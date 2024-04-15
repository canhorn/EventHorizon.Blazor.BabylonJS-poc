namespace EventHorizon.Game.Editor.Zone.Services.Connection;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

public struct ZoneAdminServiceConnectedEvent : INotification
{
    public string ZoneId { get; }

    public ZoneAdminServiceConnectedEvent(string zoneId)
    {
        ZoneId = zoneId;
    }
}

public interface ZoneAdminServiceConnectedEventObserver
    : ArgumentObserver<ZoneAdminServiceConnectedEvent> { }

public class ZoneAdminServiceConnectedEventObserverHandler
    : INotificationHandler<ZoneAdminServiceConnectedEvent>
{
    private readonly ObserverState _observer;

    public ZoneAdminServiceConnectedEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        ZoneAdminServiceConnectedEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<ZoneAdminServiceConnectedEventObserver, ZoneAdminServiceConnectedEvent>(
            notification,
            cancellationToken
        );
}
