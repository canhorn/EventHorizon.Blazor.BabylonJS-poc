namespace EventHorizon.Game.Editor.Client.Authentication.Fill;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;

using MediatR;

public struct SessionValueSetEvent : INotification
{
    public string Key { get; }

    public SessionValueSetEvent(string key)
    {
        Key = key;
    }
}

public interface SessionValueSetEventObserver
    : ArgumentObserver<SessionValueSetEvent> { }

public class SessionValueSetEventObserverHandler
    : INotificationHandler<SessionValueSetEvent>
{
    private readonly ObserverState _observer;

    public SessionValueSetEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        SessionValueSetEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<SessionValueSetEventObserver, SessionValueSetEvent>(
            notification,
            cancellationToken
        );
}
