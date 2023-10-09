namespace EventHorizon.Game.Client.Systems.Entity.Modules.Animation.Loaded;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Systems.Entity.Modules.Animation.Api;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;

using MediatR;

public struct AnimationListLoadedEvent : INotification
{
    public long ClientId { get; }
    public IEnumerable<IAnimationGroup> AnimationList { get; }

    public AnimationListLoadedEvent(
        long clientId,
        IEnumerable<IAnimationGroup> animationList
    )
    {
        ClientId = clientId;
        AnimationList = animationList;
    }
}

public interface AnimationListLoadedEventObserver
    : ArgumentObserver<AnimationListLoadedEvent> { }

public class AnimationListLoadedEventHandler
    : INotificationHandler<AnimationListLoadedEvent>
{
    private readonly ObserverState _observer;

    public AnimationListLoadedEventHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        AnimationListLoadedEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            AnimationListLoadedEventObserver,
            AnimationListLoadedEvent
        >(notification, cancellationToken);
}
