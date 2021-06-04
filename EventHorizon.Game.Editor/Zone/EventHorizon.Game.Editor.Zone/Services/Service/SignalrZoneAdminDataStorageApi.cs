namespace EventHorizon.Game.Editor.Zone.Services.Service
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Zone.Services.Api;
    using EventHorizon.Game.Editor.Zone.Services.Model;
    using Microsoft.AspNetCore.SignalR.Client;
    using Microsoft.Extensions.Primitives;

    public class SignalrZoneAdminDataStorageApi
        : ZoneAdminDataStorageApi
    {
        private readonly HubConnection? _hubConnection;

        public SignalrZoneAdminDataStorageApi(
            HubConnection? hubConnection
        )
        {
            _hubConnection = hubConnection;
        }

        public async Task<ApiResponse<Dictionary<string, object>>> All(
            CancellationToken cancellationToken
        )
        {
            if (_hubConnection.IsNull())
            {
                return new ApiResponse<Dictionary<string, object>>()
                {
                    Success = false,
                    ErrorCode = ZoneAdminErrorCodes.NOT_CONNECTED,
                };
            }

            return await _hubConnection.InvokeAsync<ApiResponse<Dictionary<string, object>>>(
                "DataStorage_All",
                cancellationToken
            );
        }

        public async Task<StandardApiResponse> Create(
            string key, 
            string type,
            object value,
            CancellationToken cancellationToken
        )
        {
            if (_hubConnection.IsNull())
            {
                return new StandardApiResponse()
                {
                    Success = false,
                    ErrorCode = ZoneAdminErrorCodes.NOT_CONNECTED,
                };
            }

            return await _hubConnection.InvokeAsync<StandardApiResponse>(
                "DataStorage_Create",
                key,
                type,
                value,
                cancellationToken
            );
        }

        public async Task<StandardApiResponse> Delete(
            string key,
            CancellationToken cancellationToken
        )
        {
            if (_hubConnection.IsNull())
            {
                return new StandardApiResponse()
                {
                    Success = false,
                    ErrorCode = ZoneAdminErrorCodes.NOT_CONNECTED,
                };
            }

            return await _hubConnection.InvokeAsync<StandardApiResponse>(
                "DataStorage_Delete",
                key,
                cancellationToken
            );
        }

        public async Task<StandardApiResponse> Update(
            string key, 
            string type,
            object value,
            CancellationToken cancellationToken
        )
        {
            if (_hubConnection.IsNull())
            {
                return new StandardApiResponse()
                {
                    Success = false,
                    ErrorCode = ZoneAdminErrorCodes.NOT_CONNECTED,
                };
            }

            return await _hubConnection.InvokeAsync<StandardApiResponse>(
                "DataStorage_Update",
                key,
                type,
                value,
                cancellationToken
            );
        }
    }
}