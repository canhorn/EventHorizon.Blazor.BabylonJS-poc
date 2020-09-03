namespace EventHorizon.Game.Client.Engine.Entity.Tracking.Query
{
    using System;
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Core.Query.Model;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using MediatR;

    public struct QueryForEntity
        : IRequest<QueryResult<IEnumerable<IObjectEntity>>>
    {
        public string Tag { get; }
        public bool Not { get; }

        public QueryForEntity(
            string tag,
            bool not = false
        )
        {
            Tag = tag;
            Not = not;
        }
    }
}
