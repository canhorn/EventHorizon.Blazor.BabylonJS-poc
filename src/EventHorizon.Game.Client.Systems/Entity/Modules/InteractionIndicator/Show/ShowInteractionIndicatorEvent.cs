namespace EventHorizon.Game.Client.Systems.Entity.Modules.InteractionIndicator.Show;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;

using MediatR;

public struct ShowInteractionIndicatorEvent : INotification
{
    public long EntityId { get; }

    public ShowInteractionIndicatorEvent(long entityId)
    {
        EntityId = entityId;
    }
}

public interface ShowInteractionIndicatorEventObserver
    : ArgumentObserver<ShowInteractionIndicatorEvent> { }

public class ShowInteractionIndicatorEventHandler
    : INotificationHandler<ShowInteractionIndicatorEvent>
{
    private readonly ObserverState _observer;

    public ShowInteractionIndicatorEventHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        ShowInteractionIndicatorEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            ShowInteractionIndicatorEventObserver,
            ShowInteractionIndicatorEvent
        >(notification, cancellationToken);
}
