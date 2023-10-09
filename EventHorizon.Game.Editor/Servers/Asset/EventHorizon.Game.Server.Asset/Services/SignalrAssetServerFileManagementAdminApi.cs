namespace EventHorizon.Game.Server.Asset.Services;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Server.Asset.Api;
using EventHorizon.Game.Server.Asset.Model;

using Microsoft.AspNetCore.SignalR.Client;

public class SignalrAssetServerFileManagementAdminApi
    : AssetServerFileManagementAdminApi
{
    private readonly HubConnection? _hubConnection;

    internal SignalrAssetServerFileManagementAdminApi(
        HubConnection? hubConnection
    )
    {
        _hubConnection = hubConnection;
    }

    public async Task<ApiResponse<FileManagementAssets>> Assets(
        CancellationToken cancellationToken
    )
    {
        if (_hubConnection.IsNotConnected())
        {
            return new ApiResponse<FileManagementAssets>
            {
                Success = false,
                ErrorCode = AssetServerAdminErrorCodes.NOT_CONNECTED,
            };
        }

        return await _hubConnection.InvokeAsync<
            ApiResponse<FileManagementAssets>
        >("FileManagement_Assets", cancellationToken: cancellationToken);
    }
}
