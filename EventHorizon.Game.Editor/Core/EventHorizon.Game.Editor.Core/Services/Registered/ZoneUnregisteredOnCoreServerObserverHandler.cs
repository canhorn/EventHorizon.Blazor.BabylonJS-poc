namespace EventHorizon.Game.Editor.Core.Services.Registered;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Observer.State;

using MediatR;

public class ZoneUnregisteredOnCoreServerObserverHandler
    : INotificationHandler<ZoneUnregisteredOnCoreServer>
{
    private readonly ObserverState _observer;

    public ZoneUnregisteredOnCoreServerObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        ZoneUnregisteredOnCoreServer notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            ZoneUnregisteredOnCoreServerObserver,
            ZoneUnregisteredOnCoreServer
        >(notification, cancellationToken);
}
