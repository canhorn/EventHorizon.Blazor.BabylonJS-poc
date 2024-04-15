namespace EventHorizon.Game.Client.Systems.Player.Query;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Query.Model;
using EventHorizon.Game.Client.Systems.Player.Api;
using MediatR;

public class QueryForCurrentPlayerHandler
    : IRequestHandler<QueryForCurrentPlayer, QueryResult<IPlayerEntity>>
{
    private readonly IPlayerState _state;

    public QueryForCurrentPlayerHandler(IPlayerState state)
    {
        _state = state;
    }

    public Task<QueryResult<IPlayerEntity>> Handle(
        QueryForCurrentPlayer request,
        CancellationToken cancellationToken
    )
    {
        var player = _state.Player;
        if (player.HasValue)
        {
            return new QueryResult<IPlayerEntity>(player.Value).FromResult();
        }
        return new QueryResult<IPlayerEntity>("current_player_not_found").FromResult();
    }
}
