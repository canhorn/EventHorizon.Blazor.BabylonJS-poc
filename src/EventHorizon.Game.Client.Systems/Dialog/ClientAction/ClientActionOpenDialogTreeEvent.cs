namespace EventHorizon.Game.Client.Systems.Dialog.ClientAction;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
using EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

[ClientAction("Systems.Dialog.OPEN_DIALOG_TREE_COMMAND")]
public struct ClientActionOpenDialogTreeEvent : IClientAction
{
    public string DialogTreeId { get; }
    public long PlayerId { get; }
    public long NpcId { get; }

    public ClientActionOpenDialogTreeEvent(IClientActionDataResolver resolver)
    {
        DialogTreeId = resolver.Resolve<string>("dialogTreeId");
        PlayerId = resolver.Resolve<long>("playerId");
        NpcId = resolver.Resolve<long>("npcId");
    }
}

public interface ClientActionOpenDialogTreeEventObserver
    : ArgumentObserver<ClientActionOpenDialogTreeEvent> { }

public class ClientActionOpenDialogTreeEventObserverHandler
    : INotificationHandler<ClientActionOpenDialogTreeEvent>
{
    private readonly ObserverState _observer;

    public ClientActionOpenDialogTreeEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        ClientActionOpenDialogTreeEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<ClientActionOpenDialogTreeEventObserver, ClientActionOpenDialogTreeEvent>(
            notification,
            cancellationToken
        );
}
