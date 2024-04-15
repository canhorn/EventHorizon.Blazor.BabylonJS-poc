namespace EventHorizon.Game.Client.Systems.Connection.Core.Account.Disconnected;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

public struct AccountDisconnectedEvent : INotification
{
    public string Code { get; }
    public Exception? Error { get; }

    public AccountDisconnectedEvent(string code, Exception? error)
    {
        Code = code;
        Error = error;
    }
}

public interface AccountDisconnectedEventObserver : ArgumentObserver<AccountDisconnectedEvent> { }

public class AccountDisconnectedEventHandler : INotificationHandler<AccountDisconnectedEvent>
{
    private readonly ObserverState _observer;

    public AccountDisconnectedEventHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        AccountDisconnectedEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<AccountDisconnectedEventObserver, AccountDisconnectedEvent>(
            notification,
            cancellationToken
        );
}
