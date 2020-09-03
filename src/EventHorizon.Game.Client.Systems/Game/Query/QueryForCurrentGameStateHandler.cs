namespace EventHorizon.Game.Client.Systems.Game.Query
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Query.Model;
    using EventHorizon.Game.Server.ServerModule.Game.Api;
    using EventHorizon.Game.Server.ServerModule.Game.Query;
    using MediatR;

    public class QueryForCurrentGameStateHandler
        : IRequestHandler<QueryForCurrentGameState, QueryResult<GameState>>
    {
        public Task<QueryResult<GameState>> Handle(
            QueryForCurrentGameState request, 
            CancellationToken cancellationToken
        )
        {
            // TODO: [GAME] - Finish Implementation
            return new QueryResult<GameState>(
                "not_found"
            ).FromResult();
        }
    }
}
