namespace EventHorizon.Game.Editor.Zone.Systems.ClientAssets.Query;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Zone.Services.Api;
using EventHorizon.Zone.Systems.ClientAssets.Model;
using EventHorizon.Zone.Systems.ClientAssets.Query;

using MediatR;

public class QueryForClientAssetByIdHandler
    : IRequestHandler<QueryForClientAssetById, CommandResult<ClientAsset>>
{
    private readonly ZoneAdminServices _zoneAdminServices;

    public QueryForClientAssetByIdHandler(ZoneAdminServices zoneAdminServices)
    {
        _zoneAdminServices = zoneAdminServices;
    }

    public async Task<CommandResult<ClientAsset>> Handle(
        QueryForClientAssetById request,
        CancellationToken cancellationToken
    )
    {
        var result = await _zoneAdminServices.Api.ClientAssets.Get(
            request.Id,
            cancellationToken
        );
        if (result.Success.IsNotTrue() || result.Result.IsNull())
        {
            return result.ErrorCode ?? ZoneAdminErrorCodes.BAD_API_REQUEST;
        }

        return result.Result;
    }
}
