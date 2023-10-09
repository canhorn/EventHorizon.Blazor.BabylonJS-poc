namespace EventHorizon.Game.Editor.Client.Zone.ClientActions.Reload;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Editor.Zone.AdminClientAction.Api;
using EventHorizon.Game.Editor.Zone.AdminClientAction.Attributes;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;

using MediatR;

[AdminClientAction(
    "SERVER_SCRIPTS_SYSTEM_COMPILING_SCRIPTS_CLIENT_ACTION_EVENT"
)]
public struct ServerScriptsSystemCompilingScriptsClientAction
    : IAdminClientAction
{
    public ServerScriptsSystemCompilingScriptsClientAction(
        IAdminClientActionDataResolver _
    ) { }
}

public interface ServerScriptsSystemCompilingScriptsClientActionObserver
    : ArgumentObserver<ServerScriptsSystemCompilingScriptsClientAction> { }

public class ServerScriptsSystemCompilingScriptsClientActionObserverHandler
    : INotificationHandler<ServerScriptsSystemCompilingScriptsClientAction>
{
    private readonly ObserverState _observer;

    public ServerScriptsSystemCompilingScriptsClientActionObserverHandler(
        ObserverState observer
    )
    {
        _observer = observer;
    }

    public Task Handle(
        ServerScriptsSystemCompilingScriptsClientAction notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            ServerScriptsSystemCompilingScriptsClientActionObserver,
            ServerScriptsSystemCompilingScriptsClientAction
        >(notification, cancellationToken);
}
