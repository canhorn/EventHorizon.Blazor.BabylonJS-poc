namespace EventHorizon.Game.Editor.Client.Zone.Selected;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;

using MediatR;

public struct ClientEntitySelectedEvent : INotification
{
    public IObjectEntityDetails Entity { get; }

    public ClientEntitySelectedEvent(IObjectEntityDetails entity)
    {
        Entity = entity;
    }
}

public interface ClientEntitySelectedEventObserver
    : ArgumentObserver<ClientEntitySelectedEvent> { }

public class ClientEntitySelectedEventObserverHandler
    : INotificationHandler<ClientEntitySelectedEvent>
{
    private readonly ObserverState _observer;

    public ClientEntitySelectedEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        ClientEntitySelectedEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            ClientEntitySelectedEventObserver,
            ClientEntitySelectedEvent
        >(notification, cancellationToken);
}
