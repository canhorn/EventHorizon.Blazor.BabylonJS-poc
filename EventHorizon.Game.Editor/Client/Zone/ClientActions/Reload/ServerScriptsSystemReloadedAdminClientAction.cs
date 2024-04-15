namespace EventHorizon.Game.Editor.Client.Zone.ClientActions.Reload;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Editor.Zone.AdminClientAction.Api;
using EventHorizon.Game.Editor.Zone.AdminClientAction.Attributes;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

[AdminClientAction("SERVER_SCRIPTS_SYSTEM_RELOADED_CLIENT_ACTION_EVENT")]
public struct ServerScriptsSystemReloadedAdminClientAction : IAdminClientAction
{
    public ServerScriptsSystemReloadedAdminClientAction(IAdminClientActionDataResolver _) { }
}

public interface ServerScriptsSystemReloadedAdminClientActionObserver
    : ArgumentObserver<ServerScriptsSystemReloadedAdminClientAction> { }

public class ServerScriptsSystemReloadedAdminClientActionObserverHandler
    : INotificationHandler<ServerScriptsSystemReloadedAdminClientAction>
{
    private readonly ObserverState _observer;

    public ServerScriptsSystemReloadedAdminClientActionObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        ServerScriptsSystemReloadedAdminClientAction notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            ServerScriptsSystemReloadedAdminClientActionObserver,
            ServerScriptsSystemReloadedAdminClientAction
        >(notification, cancellationToken);
}
