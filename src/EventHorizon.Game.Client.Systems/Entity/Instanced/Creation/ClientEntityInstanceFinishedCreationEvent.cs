namespace EventHorizon.Game.Client.Systems.Entity.Instanced.Creation;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;

using MediatR;

public struct ClientEntityInstanceFinishedCreationEvent : INotification
{
    public IObjectEntity Entity { get; }

    public ClientEntityInstanceFinishedCreationEvent(IObjectEntity entity)
    {
        Entity = entity;
    }
}

public interface ClientEntityInstanceRegisteredEventObserver
    : ArgumentObserver<ClientEntityInstanceFinishedCreationEvent> { }

public class ClientEntityInstanceRegisteredEventHandler
    : INotificationHandler<ClientEntityInstanceFinishedCreationEvent>
{
    private readonly ObserverState _observer;

    public ClientEntityInstanceRegisteredEventHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        ClientEntityInstanceFinishedCreationEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            ClientEntityInstanceRegisteredEventObserver,
            ClientEntityInstanceFinishedCreationEvent
        >(notification, cancellationToken);
}
