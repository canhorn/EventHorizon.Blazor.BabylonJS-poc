namespace EventHorizon.Game.Server.Asset.Api;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Asset.Model;

public interface AssetServerCommonAdminApi
{
    Task<ApiResponse<ArtifactListResult>> ArtifactList(CancellationToken cancellationToken);
}
