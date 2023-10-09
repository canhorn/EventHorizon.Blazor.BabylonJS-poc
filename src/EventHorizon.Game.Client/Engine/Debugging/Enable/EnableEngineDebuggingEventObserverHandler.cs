namespace EventHorizon.Game.Client.Engine.Debugging.Enable;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Observer.State;

using MediatR;

public class EnableEngineDebuggingEventObserverHandler
    : INotificationHandler<EnableEngineDebuggingEvent>
{
    private readonly ObserverState _observer;

    public EnableEngineDebuggingEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        EnableEngineDebuggingEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            EnableEngineDebuggingEventObserver,
            EnableEngineDebuggingEvent
        >(notification, cancellationToken);
}
