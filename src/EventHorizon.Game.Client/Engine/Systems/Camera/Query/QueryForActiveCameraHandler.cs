namespace EventHorizon.Game.Client.Engine.Systems.Camera.Query;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Query.Model;
using EventHorizon.Game.Client.Engine.Systems.Camera.Api;
using EventHorizon.Game.Client.Engine.Systems.Camera.Model;
using MediatR;

public class QueryForActiveCameraHandler
    : IRequestHandler<QueryForActiveCamera, QueryResult<ICamera>>
{
    private readonly ICameraState _state;

    public QueryForActiveCameraHandler(ICameraState state)
    {
        _state = state;
    }

    public Task<QueryResult<ICamera>> Handle(
        QueryForActiveCamera request,
        CancellationToken cancellationToken
    )
    {
        return new QueryResult<ICamera>(_state.ActiveCamera).FromResult();
    }
}
