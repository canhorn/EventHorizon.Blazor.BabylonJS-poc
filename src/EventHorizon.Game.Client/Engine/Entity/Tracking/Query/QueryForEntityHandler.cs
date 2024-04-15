namespace EventHorizon.Game.Client.Engine.Entity.Tracking.Query;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Query.Model;
using EventHorizon.Game.Client.Engine.Entity.Tracking.Api;
using EventHorizon.Game.Client.Engine.Lifecycle.Api;
using MediatR;

public class QueryForEntityHandler
    : IRequestHandler<QueryForEntity, QueryResult<IEnumerable<ILifecycleEntity>>>
{
    private readonly IServerEntityTrackingState _state;

    public QueryForEntityHandler(IServerEntityTrackingState state)
    {
        _state = state;
    }

    public Task<QueryResult<IEnumerable<ILifecycleEntity>>> Handle(
        QueryForEntity request,
        CancellationToken cancellationToken
    )
    {
        return new QueryResult<IEnumerable<ILifecycleEntity>>(
            _state.QueryByTag<ILifecycleEntity>(request.Tag)
        ).FromResult();
    }
}
