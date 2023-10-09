namespace EventHorizon.Game.Client.Systems.Connection.Core.Account.Connected;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Systems.Account.Api;
using EventHorizon.Game.Client.Systems.Connection.Core.Model;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;

using MediatR;

public struct AccountConnectedEvent : INotification
{
    public IAccountInfo AccountInfo { get; }

    public AccountConnectedEvent(IAccountInfo accountInfo)
    {
        AccountInfo = accountInfo;
    }
}

public interface AccountConnectedEventObserver
    : ArgumentObserver<AccountConnectedEvent> { }

public class AccountConnectedEventHandler
    : INotificationHandler<AccountConnectedEvent>
{
    private readonly ObserverState _observer;

    public AccountConnectedEventHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        AccountConnectedEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<AccountConnectedEventObserver, AccountConnectedEvent>(
            notification,
            cancellationToken
        );
}
