namespace EventHorizon.Game.Editor.Core.Services.Connection;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;

using MediatR;

public struct CoreAdminServiceConnected : INotification { }

public interface CoreAdminServiceConnectedObserver
    : ArgumentObserver<CoreAdminServiceConnected> { }

public class CoreAdminServiceConnectedObserverHandler
    : INotificationHandler<CoreAdminServiceConnected>
{
    private readonly ObserverState _observer;

    public CoreAdminServiceConnectedObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        CoreAdminServiceConnected notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            CoreAdminServiceConnectedObserver,
            CoreAdminServiceConnected
        >(notification, cancellationToken);
}
