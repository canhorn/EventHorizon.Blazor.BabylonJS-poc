﻿namespace EventHorizon.Game.Client.Systems.Local.ScreenPointer.Entity;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

public struct PointerHitEntityEvent : INotification
{
    public long EntityId { get; }

    public PointerHitEntityEvent(long entityId)
    {
        EntityId = entityId;
    }
}

public interface PointerHitEntityEventObserver : ArgumentObserver<PointerHitEntityEvent> { }

public class PointerHitEntityEventHandler : INotificationHandler<PointerHitEntityEvent>
{
    private readonly ObserverState _observer;

    public PointerHitEntityEventHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(PointerHitEntityEvent notification, CancellationToken cancellationToken) =>
        _observer.Trigger<PointerHitEntityEventObserver, PointerHitEntityEvent>(
            notification,
            cancellationToken
        );
}
