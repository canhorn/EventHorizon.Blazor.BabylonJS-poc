namespace EventHorizon.Game.Client.Systems.Map.ClientAction;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
using EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes;
using EventHorizon.Game.Client.Systems.Map.Api;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

[ClientAction("Core.Map.Created")]
public struct ClientActionCoreMapLoadedToAllEvent : IClientAction
{
    public IMapMeshDetails MapMesh { get; }

    public ClientActionCoreMapLoadedToAllEvent(IClientActionDataResolver resolver)
    {
        MapMesh = resolver.Resolve<IMapMeshDetails>("mapMesh");
    }
}

public interface ClientActionCoreMapLoadedToAllEventObserver
    : ArgumentObserver<ClientActionCoreMapLoadedToAllEvent> { }

public class ClientActionCoreMapLoadedToAllEventObserverHandler
    : INotificationHandler<ClientActionCoreMapLoadedToAllEvent>
{
    private readonly ObserverState _observer;

    public ClientActionCoreMapLoadedToAllEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        ClientActionCoreMapLoadedToAllEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            ClientActionCoreMapLoadedToAllEventObserver,
            ClientActionCoreMapLoadedToAllEvent
        >(notification, cancellationToken);
}
