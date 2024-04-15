namespace EventHorizon.Game.Editor.Zone.Services.Connection;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

public struct ZoneAdminServiceDisconnectedEvent : INotification
{
    public string ZoneId { get; }
    public string ReasonCode { get; }

    public ZoneAdminServiceDisconnectedEvent(string zoneId, string reasonCode)
    {
        ZoneId = zoneId;
        ReasonCode = reasonCode;
    }
}

public interface ZoneAdminServiceDisconnectedEventObserver
    : ArgumentObserver<ZoneAdminServiceDisconnectedEvent> { }

public class ZoneAdminServiceDisconnectedEventObserverHandler
    : INotificationHandler<ZoneAdminServiceDisconnectedEvent>
{
    private readonly ObserverState _observer;

    public ZoneAdminServiceDisconnectedEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        ZoneAdminServiceDisconnectedEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            ZoneAdminServiceDisconnectedEventObserver,
            ZoneAdminServiceDisconnectedEvent
        >(notification, cancellationToken);
}
