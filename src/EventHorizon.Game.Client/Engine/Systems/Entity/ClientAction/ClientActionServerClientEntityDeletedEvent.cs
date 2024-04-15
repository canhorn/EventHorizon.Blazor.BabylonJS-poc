namespace EventHorizon.Game.Client.Engine.Systems.Entity.ClientAction;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
using EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

[ClientAction("SERVER_CLIENT_ENTITY_DELETED_CLIENT_ACTION_EVENT")]
public struct ClientActionServerClientEntityDeletedEvent : IClientAction
{
    public string GlobalId { get; }

    public ClientActionServerClientEntityDeletedEvent(IClientActionDataResolver resolver)
    {
        GlobalId = resolver.Resolve<string>("globalId");
    }
}

public interface ClientActionServerClientEntityDeletedEventObserver
    : ArgumentObserver<ClientActionServerClientEntityDeletedEvent> { }

public class ClientActionServerClientEntityDeletedEventObserverHandler
    : INotificationHandler<ClientActionServerClientEntityDeletedEvent>
{
    private readonly ObserverState _observer;

    public ClientActionServerClientEntityDeletedEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        ClientActionServerClientEntityDeletedEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            ClientActionServerClientEntityDeletedEventObserver,
            ClientActionServerClientEntityDeletedEvent
        >(notification, cancellationToken);
}
