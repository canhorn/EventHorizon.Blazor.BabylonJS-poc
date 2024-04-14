namespace EventHorizon.Game.Editor.Zone.Systems.Player.Query;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Zone.Services.Api;
using EventHorizon.Zone.Systems.Player.Model;
using EventHorizon.Zone.Systems.Player.Query;
using MediatR;

public class QueryForZonePlayerDataHandler(ZoneAdminServices zoneAdminServices)
    : IRequestHandler<QueryForZonePlayerData, CommandResult<PlayerObjectEntityDataModel>>
{
    public async Task<CommandResult<PlayerObjectEntityDataModel>> Handle(
        QueryForZonePlayerData request,
        CancellationToken cancellationToken
    )
    {
        var result = await zoneAdminServices.Api.Player.GetData(cancellationToken);
        if (result.Success.IsNotTrue() || result.Result.IsNull())
        {
            return result.ErrorCode ?? ZoneAdminErrorCodes.BAD_API_REQUEST;
        }

        return result.Result;
    }
}
