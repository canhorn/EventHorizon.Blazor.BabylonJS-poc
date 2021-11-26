namespace EventHorizon.Game.Server.Asset.Services;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Server.Asset.Api;
using EventHorizon.Game.Server.Asset.Model;

using Microsoft.AspNetCore.SignalR.Client;

public class SignalrAssetServerExportAdminApi
    : AssetServerExportAdminApi
{
    private readonly HubConnection? _hubConnection;

    internal SignalrAssetServerExportAdminApi(
        HubConnection? hubConnection
    )
    {
        _hubConnection = hubConnection;
    }

    public async Task<
        ApiResponse<ExportTriggerResult>
    > Trigger(CancellationToken cancellationToken)
    {
        if (_hubConnection.IsNotConnected())
        {
            return new ApiResponse<ExportTriggerResult>
            {
                Success = false,
                ErrorCode =
                    AssetServerAdminErrorCodes.NOT_CONNECTED,
            };
        }

        return await _hubConnection.InvokeAsync<
            ApiResponse<ExportTriggerResult>
        >(
            "Asset_Export_Trigger",
            cancellationToken: cancellationToken
        );
    }
}
