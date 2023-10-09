namespace EventHorizon.Game.Client.Systems.Entity.Modules.Animation.Play;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;

using MediatR;

public struct PlayAnimationEvent : INotification
{
    public long ClientId { get; }
    public string Animation { get; }

    public PlayAnimationEvent(long clientId, string animation)
    {
        ClientId = clientId;
        Animation = animation;
    }
}

public interface PlayAnimationEventObserver
    : ArgumentObserver<PlayAnimationEvent> { }

public class PlayAnimationEventHandler
    : INotificationHandler<PlayAnimationEvent>
{
    private readonly ObserverState _observer;

    public PlayAnimationEventHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        PlayAnimationEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<PlayAnimationEventObserver, PlayAnimationEvent>(
            notification,
            cancellationToken
        );
}
