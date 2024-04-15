namespace EventHorizon.Game.Editor.Zone.Systems.ClientAssets.Query;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Zone.Services.Api;
using EventHorizon.Zone.Systems.ClientAssets.Model;
using EventHorizon.Zone.Systems.ClientAssets.Query;
using MediatR;

public class QueryForAllClientAssetsHandler
    : IRequestHandler<QueryForAllClientAssets, CommandResult<IEnumerable<ClientAsset>>>
{
    private readonly ZoneAdminServices _zoneAdminServices;

    public QueryForAllClientAssetsHandler(ZoneAdminServices zoneAdminServices)
    {
        _zoneAdminServices = zoneAdminServices;
    }

    public async Task<CommandResult<IEnumerable<ClientAsset>>> Handle(
        QueryForAllClientAssets request,
        CancellationToken cancellationToken
    )
    {
        var result = await _zoneAdminServices.Api.ClientAssets.All(cancellationToken);
        if (result.Success.IsNotTrue() || result.Result.IsNull())
        {
            return result.ErrorCode ?? ZoneAdminErrorCodes.BAD_API_REQUEST;
        }

        return result.Result;
    }
}
