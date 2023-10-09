namespace EventHorizon.Game.Client.Systems.Account.Query;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Systems.Account.Api;

using MediatR;

public class QueryForAccountInfoHandler
    : IRequestHandler<QueryForAccountInfo, CommandResult<IAccountInfo>>
{
    private readonly IAccountState _state;

    public QueryForAccountInfoHandler(IAccountState state)
    {
        _state = state;
    }

    public Task<CommandResult<IAccountInfo>> Handle(
        QueryForAccountInfo request,
        CancellationToken cancellationToken
    )
    {
        var user = _state.User;
        if (user == null)
        {
            return new CommandResult<IAccountInfo>(
                "user_account_info_not_found"
            ).FromResult();
        }
        return new CommandResult<IAccountInfo>(user).FromResult();
    }
}
