namespace EventHorizon.Game.Server.Asset.Api;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Server.Asset.Model;

public interface AssetServerFileManagementAdminApi
{
    Task<ApiResponse<FileManagementAssets>> Assets(
        CancellationToken cancellationToken
    );
}
