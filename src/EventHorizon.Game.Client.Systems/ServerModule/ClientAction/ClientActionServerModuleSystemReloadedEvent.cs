namespace EventHorizon.Game.Client.Systems.ServerModule.ClientAction;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
using EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes;
using EventHorizon.Game.Client.Systems.ServerModule.Api;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

[ClientAction("SERVER_MODULE_SYSTEM_RELOADED_CLIENT_ACTION_EVENT")]
public struct ClientActionServerModuleSystemReloadedEvent : IClientAction
{
    public IEnumerable<IServerModuleScripts> ServerModuleScriptsList { get; }

    public ClientActionServerModuleSystemReloadedEvent(IClientActionDataResolver resolver)
    {
        ServerModuleScriptsList = resolver.Resolve<List<ServerModuleScriptsModel>>(
            "serverModuleScriptList"
        );
    }
}

public interface ClientActionServerModuleSystemReloadedEventObserver
    : ArgumentObserver<ClientActionServerModuleSystemReloadedEvent> { }

public class ClientActionServerModuleSystemReloadedEventObserverHandler
    : INotificationHandler<ClientActionServerModuleSystemReloadedEvent>
{
    private readonly ObserverState _observer;

    public ClientActionServerModuleSystemReloadedEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        ClientActionServerModuleSystemReloadedEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            ClientActionServerModuleSystemReloadedEventObserver,
            ClientActionServerModuleSystemReloadedEvent
        >(notification, cancellationToken);
}
