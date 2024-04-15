namespace EventHorizon.Game.Client.Systems.Entity.ClientAction;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.State;
using MediatR;

public class ClientActionEntityMoveEventHandler : INotificationHandler<ClientActionEntityMoveEvent>
{
    private readonly ObserverState _observer;

    public ClientActionEntityMoveEventHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        ClientActionEntityMoveEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<ClientActionEntityMoveEventObserver, ClientActionEntityMoveEvent>(
            notification,
            cancellationToken
        );
}
