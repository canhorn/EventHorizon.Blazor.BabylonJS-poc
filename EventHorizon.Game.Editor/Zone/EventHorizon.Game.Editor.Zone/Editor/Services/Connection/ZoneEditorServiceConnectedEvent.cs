namespace EventHorizon.Game.Editor.Zone.Editor.Services.Connection;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

public struct ZoneEditorServiceConnectedEvent : INotification
{
    public string ZoneId { get; }

    public ZoneEditorServiceConnectedEvent(string zoneId)
    {
        ZoneId = zoneId;
    }
}

public interface ZoneEditorServiceConnectedEventObserver
    : ArgumentObserver<ZoneEditorServiceConnectedEvent> { }

public class ZoneEditorServiceConnectedEventObserverHandler
    : INotificationHandler<ZoneEditorServiceConnectedEvent>
{
    private readonly ObserverState _observer;

    public ZoneEditorServiceConnectedEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        ZoneEditorServiceConnectedEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<ZoneEditorServiceConnectedEventObserver, ZoneEditorServiceConnectedEvent>(
            notification,
            cancellationToken
        );
}
