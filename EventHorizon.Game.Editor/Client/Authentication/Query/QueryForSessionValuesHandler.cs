namespace EventHorizon.Game.Editor.Client.Authentication.Query;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.Authentication.Api;

using MediatR;

public class QueryForSessionValuesHandler
    : IRequestHandler<QueryForSessionValues, CommandResult<SessionValues>>
{
    private readonly EditorAuthenticationState _state;

    public QueryForSessionValuesHandler(EditorAuthenticationState state)
    {
        _state = state;
    }

    public Task<CommandResult<SessionValues>> Handle(
        QueryForSessionValues request,
        CancellationToken cancellationToken
    )
    {
        return new CommandResult<SessionValues>(_state.Session).FromResult();
    }
}
