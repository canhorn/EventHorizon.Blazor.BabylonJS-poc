namespace EventHorizon.Game.Client.Systems.Player.ClientAction;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Observer.State;

using MediatR;

public class ClientActionPlayerSystemReloadedEventHandler
    : INotificationHandler<ClientActionPlayerSystemReloadedEvent>
{
    private readonly ObserverState _observer;

    public ClientActionPlayerSystemReloadedEventHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        ClientActionPlayerSystemReloadedEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            ClientActionPlayerSystemReloadedEventObserver,
            ClientActionPlayerSystemReloadedEvent
        >(notification, cancellationToken);
}
