namespace EventHorizon.Game.Editor.Zone.Services.Api;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Editor.Zone.Services.Model;
using EventHorizon.Game.Editor.Zone.Systems.ArtifactManagement.Model;

public interface ZoneAdminArtifactManagementApi
{
    Task<ApiResponse<TriggerZoneArtifactBackupApiResult>> TriggerBackup(
        CancellationToken cancellationToken
    );
    Task<ApiResponse<TriggerZoneArtifactExportApiResult>> TriggerExport(
        CancellationToken cancellationToken
    );
    Task<ApiResponse<TriggerZoneArtifactImportApiResult>> TriggerImport(
        string importArtifactUrl,
        CancellationToken cancellationToken
    );
}
