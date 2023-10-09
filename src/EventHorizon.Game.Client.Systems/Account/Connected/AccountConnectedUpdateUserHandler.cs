namespace EventHorizon.Game.Client.Systems.Account.Connected;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Systems.Account.Api;
using EventHorizon.Game.Client.Systems.Account.Changed;
using EventHorizon.Game.Client.Systems.Connection.Core.Account.Connected;

using MediatR;

public class AccountConnectedUpdateUserHandler
    : INotificationHandler<AccountConnectedEvent>
{
    private readonly IMediator _mediator;
    private readonly IAccountState _state;

    public AccountConnectedUpdateUserHandler(
        IMediator mediator,
        IAccountState state
    )
    {
        _mediator = mediator;
        _state = state;
    }

    public Task Handle(
        AccountConnectedEvent notification,
        CancellationToken cancellationToken
    )
    {
        _state.SetAccountUser(notification.AccountInfo);
        return _mediator.Publish(new AccountChangedEvent());
    }
}
