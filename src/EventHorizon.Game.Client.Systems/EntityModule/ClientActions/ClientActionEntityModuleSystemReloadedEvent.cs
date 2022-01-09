namespace EventHorizon.Game.Client.Systems.EntityModule.ClientActions;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
using EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes;
using EventHorizon.Game.Client.Systems.EntityModule.Api;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;

using MediatR;

[ClientAction("ENTITY_MODULE_SYSTEM_RELOADED_CLIENT_ACTION_EVENT")]
public record ClientActionEntityModuleSystemReloadedEvent
    : IClientAction
{
    public IEnumerable<EntityModuleScripts> BaseEntityScriptModuleList { get; }
    public IEnumerable<EntityModuleScripts> PlayerEntityScriptModuleList { get; }

    public ClientActionEntityModuleSystemReloadedEvent(
        IClientActionDataResolver resolver
    )
    {
        BaseEntityScriptModuleList = resolver.Resolve<List<EntityModuleScriptsModel>>(
            "baseEntityScriptModuleList"
        );
        PlayerEntityScriptModuleList = resolver.Resolve<List<EntityModuleScriptsModel>>(
            "playerEntityScriptModuleList"
        );
    }
}

public interface ClientActionEntityModuleSystemReloadedEventObserver
    : ArgumentObserver<ClientActionEntityModuleSystemReloadedEvent>
{
}

public class ClientActionEntityModuleSystemReloadedEventObserverHandler
    : INotificationHandler<ClientActionEntityModuleSystemReloadedEvent>
{
    private readonly ObserverState _observer;

    public ClientActionEntityModuleSystemReloadedEventObserverHandler(
        ObserverState observer
    )
    {
        _observer = observer;
    }

    public Task Handle(
        ClientActionEntityModuleSystemReloadedEvent notification,
        CancellationToken cancellationToken
    ) => _observer.Trigger<ClientActionEntityModuleSystemReloadedEventObserver, ClientActionEntityModuleSystemReloadedEvent>(
        notification,
        cancellationToken
    );
}
