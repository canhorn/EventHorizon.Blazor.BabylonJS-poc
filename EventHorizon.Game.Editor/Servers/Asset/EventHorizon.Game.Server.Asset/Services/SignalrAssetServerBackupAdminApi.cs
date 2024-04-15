namespace EventHorizon.Game.Server.Asset.Services;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Asset.Api;
using EventHorizon.Game.Server.Asset.Model;
using Microsoft.AspNetCore.SignalR.Client;

public class SignalrAssetServerBackupAdminApi : AssetServerBackupAdminApi
{
    private readonly HubConnection? _hubConnection;

    internal SignalrAssetServerBackupAdminApi(HubConnection? hubConnection)
    {
        _hubConnection = hubConnection;
    }

    public async Task<ApiResponse<BackupTriggerResult>> Trigger(CancellationToken cancellationToken)
    {
        if (_hubConnection.IsNotConnected())
        {
            return new ApiResponse<BackupTriggerResult>
            {
                Success = false,
                ErrorCode = AssetServerAdminErrorCodes.NOT_CONNECTED,
            };
        }

        return await _hubConnection.InvokeAsync<ApiResponse<BackupTriggerResult>>(
            "Asset_Backup_Trigger",
            cancellationToken: cancellationToken
        );
    }
}
