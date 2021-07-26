namespace EventHorizon.Game.Editor.Zone.Services.Service
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Zone.Services.Api;
    using EventHorizon.Game.Editor.Zone.Services.Model;
    using EventHorizon.Zone.Systems.ClientAssets.Model;
    using Microsoft.AspNetCore.SignalR.Client;

    public class SignalrZoneAdminClientAssetsApi
        : ZoneAdminClientAssetsApi
    {
        private readonly HubConnection? _hubConnection;

        public SignalrZoneAdminClientAssetsApi(
            HubConnection? hubConnection
        )
        {
            _hubConnection = hubConnection;
        }

        public async Task<ApiResponse<List<ClientAsset>>> All(
            CancellationToken cancellationToken
        )
        {
            if (_hubConnection.IsNotConnected())
            {
                return new ApiResponse<List<ClientAsset>>()
                {
                    Success = false,
                    ErrorCode = ZoneAdminErrorCodes.NOT_CONNECTED,
                };
            }

            return await _hubConnection.InvokeAsync<ApiResponse<List<ClientAsset>>>(
                "ClientAssets_All",
                cancellationToken
            );
        }

        public async Task<ApiResponse<ClientAsset>> Get(
            string id,
            CancellationToken cancellationToken
        )
        {
            if (_hubConnection.IsNotConnected())
            {
                return new ApiResponse<ClientAsset>()
                {
                    Success = false,
                    ErrorCode = ZoneAdminErrorCodes.NOT_CONNECTED,
                };
            }

            return await _hubConnection.InvokeAsync<ApiResponse<ClientAsset>>(
                "ClientAssets_Get",
                id,
                cancellationToken
            );
        }

        public async Task<StandardApiResponse> Create(
            ClientAsset clientAsset,
            CancellationToken cancellationToken
        )
        {
            if (_hubConnection.IsNotConnected())
            {
                return NotConnectedResponse();
            }

            return await _hubConnection.InvokeAsync<StandardApiResponse>(
                "ClientAssets_Create",
                clientAsset,
                cancellationToken
            );
        }

        public async Task<StandardApiResponse> Delete(
            string id,
            CancellationToken cancellationToken
        )
        {
            if (_hubConnection.IsNotConnected())
            {
                return NotConnectedResponse();
            }

            return await _hubConnection.InvokeAsync<StandardApiResponse>(
                "ClientAssets_Delete",
                id,
                cancellationToken
            );
        }

        public async Task<StandardApiResponse> Update(
            ClientAsset clientAsset,
            CancellationToken cancellationToken
        )
        {
            if (_hubConnection.IsNotConnected())
            {
                return NotConnectedResponse();
            }

            return await _hubConnection.InvokeAsync<StandardApiResponse>(
                "ClientAssets_Update",
                clientAsset,
                cancellationToken
            );
        }

        private static StandardApiResponse NotConnectedResponse()
            => new()
            {
                Success = false,
                ErrorCode = ZoneAdminErrorCodes.NOT_CONNECTED,
            };
    }
}
