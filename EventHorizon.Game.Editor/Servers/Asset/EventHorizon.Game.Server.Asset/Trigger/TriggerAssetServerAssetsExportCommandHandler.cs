namespace EventHorizon.Game.Server.Asset.Trigger;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Server.Asset.Api;
using EventHorizon.Game.Server.Asset.Model;
using MediatR;

public class TriggerAssetServerAssetsExportCommandHandler
    : IRequestHandler<TriggerAssetServerAssetsExportCommand, CommandResult<ExportTriggerResult>>
{
    private readonly AssetServerAdminService _service;

    public TriggerAssetServerAssetsExportCommandHandler(AssetServerAdminService service)
    {
        _service = service;
    }

    public async Task<CommandResult<ExportTriggerResult>> Handle(
        TriggerAssetServerAssetsExportCommand request,
        CancellationToken cancellationToken
    )
    {
        var apiResult = await _service.ExportApi.Trigger(cancellationToken);

        if (!apiResult.Success || apiResult.Result.IsNull())
        {
            return new(apiResult.ErrorCode ?? AssetServerAdminErrorCodes.BAD_API_REQUEST);
        }

        return new(apiResult.Result);
    }
}
