namespace EventHorizon.Game.Editor.Core.Services.Registered;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Observer.State;

using MediatR;

public class ZoneRegisteredOnCoreServerObserverHandler
    : INotificationHandler<ZoneRegisteredOnCoreServer>
{
    private readonly ObserverState _observer;

    public ZoneRegisteredOnCoreServerObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        ZoneRegisteredOnCoreServer notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            ZoneRegisteredOnCoreServerObserver,
            ZoneRegisteredOnCoreServer
        >(notification, cancellationToken);
}
