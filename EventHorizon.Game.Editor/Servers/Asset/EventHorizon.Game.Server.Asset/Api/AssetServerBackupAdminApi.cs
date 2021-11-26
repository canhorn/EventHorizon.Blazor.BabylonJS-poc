namespace EventHorizon.Game.Server.Asset.Api;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Server.Asset.Model;

public interface AssetServerBackupAdminApi
{
    Task<ApiResponse<BackupTriggerResult>> Trigger(
        CancellationToken cancellationToken
    );
}
