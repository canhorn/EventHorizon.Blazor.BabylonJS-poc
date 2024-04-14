namespace EventHorizon.Game.Editor.Zone.Systems.Player.Save;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Zone.Services.Api;
using EventHorizon.Zone.Systems.Player.Model;
using EventHorizon.Zone.Systems.Player.Save;
using MediatR;

public class SaveZonePlayerDataCommandHandler(ZoneAdminServices zoneAdminServices)
    : IRequestHandler<SaveZonePlayerDataCommand, CommandResult<PlayerObjectEntityDataModel>>
{
    public async Task<CommandResult<PlayerObjectEntityDataModel>> Handle(
        SaveZonePlayerDataCommand request,
        CancellationToken cancellationToken
    )
    {
        var result = await zoneAdminServices.Api.Player.SaveData(
            request.PlayerData,
            cancellationToken
        );
        if (result.Success.IsNotTrue())
        {
            return result.ErrorCode ?? ZoneAdminErrorCodes.BAD_API_REQUEST;
        }

        var dataResult = await zoneAdminServices.Api.Player.GetData(cancellationToken);
        if (dataResult.Success.IsNotTrue() || dataResult.Result.IsNull())
        {
            return result.ErrorCode ?? ZoneAdminErrorCodes.BAD_API_REQUEST;
        }

        return dataResult.Result;
    }
}
