namespace EventHorizon.Game.Client.Systems.Entity.Modules.InteractionIndicator.Clear;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;

using MediatR;

public struct ClearInteractionIndicatorEvent : INotification { }

public interface ClearInteractionIndicatorEventObserver
    : ArgumentObserver<ClearInteractionIndicatorEvent> { }

public class ClearInteractionIndicatorEventHandler
    : INotificationHandler<ClearInteractionIndicatorEvent>
{
    private readonly ObserverState _observer;

    public ClearInteractionIndicatorEventHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        ClearInteractionIndicatorEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            ClearInteractionIndicatorEventObserver,
            ClearInteractionIndicatorEvent
        >(notification, cancellationToken);
}
