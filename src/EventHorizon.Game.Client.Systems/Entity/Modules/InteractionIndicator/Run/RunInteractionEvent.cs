namespace EventHorizon.Game.Client.Systems.Entity.Modules.InteractionIndicator.Run;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;

using MediatR;

public struct RunInteractionEvent : INotification { }

public interface RunInteractionEventObserver
    : ArgumentObserver<RunInteractionEvent> { }

public class RunInteractionEventObserverHandler
    : INotificationHandler<RunInteractionEvent>
{
    private readonly ObserverState _observer;

    public RunInteractionEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        RunInteractionEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<RunInteractionEventObserver, RunInteractionEvent>(
            notification,
            cancellationToken
        );
}
