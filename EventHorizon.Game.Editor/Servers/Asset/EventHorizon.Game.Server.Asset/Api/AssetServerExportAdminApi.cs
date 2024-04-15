namespace EventHorizon.Game.Server.Asset.Api;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Asset.Model;

public interface AssetServerExportAdminApi
{
    Task<ApiResponse<ExportTriggerResult>> Trigger(CancellationToken cancellationToken);
}
