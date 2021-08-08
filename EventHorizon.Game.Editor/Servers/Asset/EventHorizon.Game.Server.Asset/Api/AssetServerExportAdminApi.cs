namespace EventHorizon.Game.Server.Asset.Api
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Server.Asset.Model;

    public interface AssetServerExportAdminApi
    {
        Task<ApiResponse<IEnumerable<ExportArtifact>>> ArtifactList(
            CancellationToken cancellationToken
        );

        Task<ApiResponse<ExportTriggerResult>> Trigger(
            CancellationToken cancellationToken
        );
    }
}
