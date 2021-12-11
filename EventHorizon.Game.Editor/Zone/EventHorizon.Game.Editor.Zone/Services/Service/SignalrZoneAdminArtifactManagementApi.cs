namespace EventHorizon.Game.Editor.Zone.Services.Service;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Editor.Zone.Services.Api;
using EventHorizon.Game.Editor.Zone.Services.Model;
using EventHorizon.Game.Editor.Zone.Systems.ArtifactManagement.Model;

using Microsoft.AspNetCore.SignalR.Client;

public class SignalrZoneAdminArtifactManagementApi
    : ZoneAdminArtifactManagementApi
{
    private readonly HubConnection? _hubConnection;

    public SignalrZoneAdminArtifactManagementApi(
        HubConnection? hubConnection
    )
    {
        _hubConnection = hubConnection;
    }

    public async Task<ApiResponse<TriggerZoneArtifactBackupApiResult>> TriggerBackup(
        CancellationToken cancellationToken
    )
    {
        if (_hubConnection.IsNotConnected())
        {
            return new()
            {
                Success = false,
                ErrorCode = ZoneAdminErrorCodes.NOT_CONNECTED,
            };
        }

        return await _hubConnection.InvokeAsync<ApiResponse<TriggerZoneArtifactBackupApiResult>>(
            "ArtifactManagement_TriggerBackup",
            cancellationToken
        );
    }

    public async Task<ApiResponse<TriggerZoneArtifactExportApiResult>> TriggerExport(
        CancellationToken cancellationToken
    )
    {
        if (_hubConnection.IsNotConnected())
        {
            return new()
            {
                Success = false,
                ErrorCode = ZoneAdminErrorCodes.NOT_CONNECTED,
            };
        }

        return await _hubConnection.InvokeAsync<ApiResponse<TriggerZoneArtifactExportApiResult>>(
            "ArtifactManagement_TriggerExport",
            cancellationToken
        );
    }

    public async Task<ApiResponse<TriggerZoneArtifactImportApiResult>> TriggerImport(
        string importArtifactUrl,
        CancellationToken cancellationToken
    )
    {
        if (_hubConnection.IsNotConnected())
        {
            return new()
            {
                Success = false,
                ErrorCode = ZoneAdminErrorCodes.NOT_CONNECTED,
            };
        }

        return await _hubConnection.InvokeAsync<ApiResponse<TriggerZoneArtifactImportApiResult>>(
            "ArtifactManagement_TriggerImport",
            importArtifactUrl,
            cancellationToken
        );
    }
}
