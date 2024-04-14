namespace EventHorizon.Game.Editor.Zone.Services.Service;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Editor.Zone.Services.Api;
using EventHorizon.Game.Editor.Zone.Services.Model;
using EventHorizon.Zone.Systems.Player.Model;
using Microsoft.AspNetCore.SignalR.Client;

public sealed class SignalrZoneAdminPlayerApi : ZoneAdminPlayerApi
{
    private readonly HubConnection? _hubConnection;

    internal SignalrZoneAdminPlayerApi(HubConnection? hubConnection)
    {
        _hubConnection = hubConnection;
    }

    public async Task<ApiResponse<PlayerObjectEntityDataModel>> GetData(
        CancellationToken cancellationToken
    )
    {
        if (_hubConnection.IsNotConnected())
        {
            return new ApiResponse<PlayerObjectEntityDataModel>()
            {
                Success = false,
                ErrorCode = ZoneAdminErrorCodes.NOT_CONNECTED,
            };
        }
        return await _hubConnection.InvokeAsync<ApiResponse<PlayerObjectEntityDataModel>>(
            "Player_GetData",
            cancellationToken
        );
    }

    public async Task<StandardApiResponse> SaveData(
        PlayerObjectEntityDataModel playerData,
        CancellationToken cancellationToken
    )
    {
        if (_hubConnection.IsNotConnected())
        {
            return new StandardApiResponse()
            {
                Success = false,
                ErrorCode = ZoneAdminErrorCodes.NOT_CONNECTED,
            };
        }

        return await _hubConnection.InvokeAsync<StandardApiResponse>(
            "Player_SaveData",
            playerData,
            cancellationToken
        );
    }
}
