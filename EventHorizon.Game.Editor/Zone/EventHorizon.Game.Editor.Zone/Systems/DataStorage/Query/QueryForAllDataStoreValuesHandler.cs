namespace EventHorizon.Game.Editor.Zone.Systems.DataStorage.Query;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Zone.Services.Api;
using EventHorizon.Zone.Systems.DataStorage.Query;
using MediatR;

public class QueryForAllDataStoreValuesHandler
    : IRequestHandler<QueryForAllDataStoreValues, CommandResult<IDictionary<string, object>>>
{
    private readonly ZoneAdminServices _zoneAdminServices;

    public QueryForAllDataStoreValuesHandler(ZoneAdminServices zoneAdminServices)
    {
        _zoneAdminServices = zoneAdminServices;
    }

    public async Task<CommandResult<IDictionary<string, object>>> Handle(
        QueryForAllDataStoreValues request,
        CancellationToken cancellationToken
    )
    {
        var result = await _zoneAdminServices.Api.DataStorage.All(cancellationToken);
        if (result.Success.IsNotTrue() || result.Result.IsNull())
        {
            return result.ErrorCode ?? ZoneAdminErrorCodes.BAD_API_REQUEST;
        }

        return result.Result;
    }
}
