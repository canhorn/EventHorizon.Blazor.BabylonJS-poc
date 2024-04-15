namespace EventHorizon.Game.Client.Systems.Account.Changed;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

public struct AccountChangedEvent : INotification { }

public interface AccountChangedEventObserver : ArgumentObserver<AccountChangedEvent> { }

public class AccountChangedEventHandler : INotificationHandler<AccountChangedEvent>
{
    private readonly ObserverState _observer;

    public AccountChangedEventHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(AccountChangedEvent notification, CancellationToken cancellationToken) =>
        _observer.Trigger<AccountChangedEventObserver, AccountChangedEvent>(
            notification,
            cancellationToken
        );
}
