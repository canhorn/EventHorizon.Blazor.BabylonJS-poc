namespace EventHorizon.Game.Server.Game.Query
{
    using System;
    using EventHorizon.Game.Client.Core.Query.Model;
    using EventHorizon.Game.Server.Game.Api;
    using MediatR;

    public struct QueryForCurrentGameState
        : IRequest<QueryResult<GameState>>
    {
    }
}
