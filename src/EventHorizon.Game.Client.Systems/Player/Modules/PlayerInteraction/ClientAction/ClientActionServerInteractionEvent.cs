namespace EventHorizon.Game.Client.Systems.Player.Modules.PlayerInteraction.ClientAction;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
using EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;

using MediatR;

// TODO: Look at making this just a Client Action
[ClientAction("SERVER_INTERACTION_CLIENT_ACTION_EVENT")]
public class ClientActionServerInteractionEvent : IClientAction
{
    public string CommandType { get; }
    public IDictionary<string, object> Data { get; }

    public ClientActionServerInteractionEvent(
        IClientActionDataResolver resolver
    )
    {
        CommandType = resolver.Resolve<string>("commandType");
        Data = resolver.Resolve<Dictionary<string, object>>("data");
    }
}

public interface ClientActionServerInteractionEventObserver
    : ArgumentObserver<ClientActionServerInteractionEvent> { }

public class ClientActionServerInteractionEventHandler
    : INotificationHandler<ClientActionServerInteractionEvent>
{
    private readonly ObserverState _observer;

    public ClientActionServerInteractionEventHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        ClientActionServerInteractionEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            ClientActionServerInteractionEventObserver,
            ClientActionServerInteractionEvent
        >(notification, cancellationToken);
}
