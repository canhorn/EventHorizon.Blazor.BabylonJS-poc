namespace EventHorizon.Game.Client.Engine.Systems.Entity.ClientAction;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
using EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;

using MediatR;

[ClientAction("SERVER_CLIENT_ENTITY_CHANGED_CLIENT_ACTION_EVENT")]
public struct ClientActionServerClientEntityChangedEvent : IClientAction
{
    public IObjectEntityDetails Details { get; set; }

    public ClientActionServerClientEntityChangedEvent(
        IClientActionDataResolver resolver
    )
    {
        Details = resolver.Resolve<IObjectEntityDetails>("details");
    }
}

public interface ServerClientEntityChangedEventObserver
    : ArgumentObserver<ClientActionServerClientEntityChangedEvent> { }

public class ServerClientEntityChangedEventObserverHandler
    : INotificationHandler<ClientActionServerClientEntityChangedEvent>
{
    private readonly ObserverState _observer;

    public ServerClientEntityChangedEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        ClientActionServerClientEntityChangedEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            ServerClientEntityChangedEventObserver,
            ClientActionServerClientEntityChangedEvent
        >(notification, cancellationToken);
}
