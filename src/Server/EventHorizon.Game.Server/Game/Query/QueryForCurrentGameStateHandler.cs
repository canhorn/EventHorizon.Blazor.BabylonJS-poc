namespace EventHorizon.Game.Server.Game.Query
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Query.Model;
    using EventHorizon.Game.Server.Game.Api;
    using MediatR;

    public class QueryForCurrentGameStateHandler
        : IRequestHandler<QueryForCurrentGameState, QueryResult<GameState>>
    {
        private readonly ServerGameState _state;

        public QueryForCurrentGameStateHandler(
            ServerGameState state
        )
        {
            _state = state;
        }

        public Task<QueryResult<GameState>> Handle(
            QueryForCurrentGameState request, 
            CancellationToken cancellationToken
        )
        {
            return new QueryResult<GameState>(
                _state.GameState
            ).FromResult();
        }
    }
}
