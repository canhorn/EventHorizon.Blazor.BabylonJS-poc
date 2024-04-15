namespace EventHorizon.Game.Client.Systems.Entity.Modules.SelectedCompanionIndicator.Selected;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

public struct CompanionSelectedEvent : INotification
{
    public long EntityId { get; }

    public CompanionSelectedEvent(long entityId)
    {
        EntityId = entityId;
    }
}

public interface CompanionSelectedEventObserver : ArgumentObserver<CompanionSelectedEvent> { }

public class CompanionSelectedEventObserverHandler : INotificationHandler<CompanionSelectedEvent>
{
    private readonly ObserverState _observer;

    public CompanionSelectedEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(CompanionSelectedEvent notification, CancellationToken cancellationToken) =>
        _observer.Trigger<CompanionSelectedEventObserver, CompanionSelectedEvent>(
            notification,
            cancellationToken
        );
}
