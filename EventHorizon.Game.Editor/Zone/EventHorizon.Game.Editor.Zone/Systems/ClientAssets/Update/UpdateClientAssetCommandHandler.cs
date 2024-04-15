namespace EventHorizon.Game.Editor.Zone.Systems.ClientAssets.Update;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Zone.Services.Api;
using EventHorizon.Zone.Systems.ClientAssets.Update;
using MediatR;

public class UpdateClientAssetCommandHandler
    : IRequestHandler<UpdateClientAssetCommand, StandardCommandResult>
{
    private readonly ZoneAdminServices _zoneAdminServices;

    public UpdateClientAssetCommandHandler(ZoneAdminServices zoneAdminServices)
    {
        _zoneAdminServices = zoneAdminServices;
    }

    public async Task<StandardCommandResult> Handle(
        UpdateClientAssetCommand request,
        CancellationToken cancellationToken
    )
    {
        var result = await _zoneAdminServices.Api.ClientAssets.Update(
            request.ClientAsset,
            cancellationToken
        );
        if (result.Success.IsNotTrue())
        {
            return result.ErrorCode ?? ZoneAdminErrorCodes.BAD_API_REQUEST;
        }

        return new();
    }
}
