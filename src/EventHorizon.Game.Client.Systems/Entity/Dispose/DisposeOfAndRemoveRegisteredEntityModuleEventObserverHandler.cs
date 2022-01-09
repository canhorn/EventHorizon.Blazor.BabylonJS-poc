namespace EventHorizon.Game.Client.Systems.Entity.Dispose;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Observer.State;

using MediatR;

public class DisposeOfAndRemoveRegisteredEntityModuleEventObserverHandler
    : INotificationHandler<DisposeOfAndRemoveRegisteredEntityModuleEvent>
{
    private readonly ObserverState _observer;

    public DisposeOfAndRemoveRegisteredEntityModuleEventObserverHandler(
        ObserverState observer
    )
    {
        _observer = observer;
    }

    public Task Handle(
        DisposeOfAndRemoveRegisteredEntityModuleEvent notification,
        CancellationToken cancellationToken
    ) => _observer.Trigger<DisposeOfAndRemoveRegisteredEntityModuleEventObserver, DisposeOfAndRemoveRegisteredEntityModuleEvent>(
        notification,
        cancellationToken
    );
}
