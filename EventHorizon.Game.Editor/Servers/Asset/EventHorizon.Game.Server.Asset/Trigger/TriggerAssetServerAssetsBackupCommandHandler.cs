namespace EventHorizon.Game.Server.Asset.Trigger;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Server.Asset.Api;
using EventHorizon.Game.Server.Asset.Model;

using MediatR;

public class TriggerAssetServerAssetsBackupCommandHandler
    : IRequestHandler<
        TriggerAssetServerAssetsBackupCommand,
        CommandResult<BackupTriggerResult>
    >
{
    private readonly AssetServerAdminService _service;

    public TriggerAssetServerAssetsBackupCommandHandler(
        AssetServerAdminService service
    )
    {
        _service = service;
    }

    public async Task<CommandResult<BackupTriggerResult>> Handle(
        TriggerAssetServerAssetsBackupCommand request,
        CancellationToken cancellationToken
    )
    {
        var apiResult = await _service.BackupApi.Trigger(cancellationToken);

        if (!apiResult.Success || apiResult.Result.IsNull())
        {
            return new(
                apiResult.ErrorCode
                    ?? AssetServerAdminErrorCodes.BAD_API_REQUEST
            );
        }

        return new(apiResult.Result);
    }
}
