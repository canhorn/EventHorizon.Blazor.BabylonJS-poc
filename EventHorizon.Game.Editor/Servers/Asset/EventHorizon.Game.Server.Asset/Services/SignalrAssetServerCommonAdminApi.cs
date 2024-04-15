namespace EventHorizon.Game.Server.Asset.Services;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Asset.Api;
using EventHorizon.Game.Server.Asset.Model;
using Microsoft.AspNetCore.SignalR.Client;

public class SignalrAssetServerCommonAdminApi : AssetServerCommonAdminApi
{
    private readonly HubConnection? _hubConnection;

    internal SignalrAssetServerCommonAdminApi(HubConnection? hubConnection)
    {
        _hubConnection = hubConnection;
    }

    public async Task<ApiResponse<ArtifactListResult>> ArtifactList(
        CancellationToken cancellationToken
    )
    {
        if (_hubConnection.IsNotConnected())
        {
            return new ApiResponse<ArtifactListResult>
            {
                Success = false,
                ErrorCode = AssetServerAdminErrorCodes.NOT_CONNECTED,
            };
        }

        return await _hubConnection.InvokeAsync<ApiResponse<ArtifactListResult>>(
            "ArtifactList",
            cancellationToken: cancellationToken
        );
    }
}
