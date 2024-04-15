namespace EventHorizon.Game.Client.Systems.EntityModule.Register;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.State;
using MediatR;

public class BaseEntityModulesChangedEventObserverHandler
    : INotificationHandler<BaseEntityModulesChangedEvent>
{
    private readonly ObserverState _observer;

    public BaseEntityModulesChangedEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        BaseEntityModulesChangedEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<BaseEntityModulesChangedEventObserver, BaseEntityModulesChangedEvent>(
            notification,
            cancellationToken
        );
}
