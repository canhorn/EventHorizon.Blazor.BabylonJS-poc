namespace EventHorizon.Game.Editor.Client.Zone.ClientActions.Reload;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Editor.Zone.AdminClientAction.Api;
using EventHorizon.Game.Editor.Zone.AdminClientAction.Attributes;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

[AdminClientAction("SERVER_SCRIPTS_SYSTEM_FINISHED_SCRIPTS_COMPILE_CLIENT_ACTION_EVENT")]
public struct ServerScriptsSystemFinishedScriptsCompileClientAction : IAdminClientAction
{
    public ServerScriptsSystemFinishedScriptsCompileClientAction(IAdminClientActionDataResolver _)
    { }
}

public interface ServerScriptsSystemFinishedScriptsCompileClientActionObserver
    : ArgumentObserver<ServerScriptsSystemFinishedScriptsCompileClientAction> { }

public class ServerScriptsSystemFinishedScriptsCompileClientActionObserverHandler
    : INotificationHandler<ServerScriptsSystemFinishedScriptsCompileClientAction>
{
    private readonly ObserverState _observer;

    public ServerScriptsSystemFinishedScriptsCompileClientActionObserverHandler(
        ObserverState observer
    )
    {
        _observer = observer;
    }

    public Task Handle(
        ServerScriptsSystemFinishedScriptsCompileClientAction notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            ServerScriptsSystemFinishedScriptsCompileClientActionObserver,
            ServerScriptsSystemFinishedScriptsCompileClientAction
        >(notification, cancellationToken);
}
