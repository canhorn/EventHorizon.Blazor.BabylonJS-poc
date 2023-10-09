namespace EventHorizon.Game.Server.ServerModule.Game.Query
{
    using System;
    using EventHorizon.Game.Client.Core.Query.Model;
    using EventHorizon.Game.Server.ServerModule.Game.Api;
    using MediatR;

    // TODO: [Game] - Finished Game Implementation
    public struct QueryForCurrentGameState
        : IRequest<QueryResult<GameState>> { }
}
