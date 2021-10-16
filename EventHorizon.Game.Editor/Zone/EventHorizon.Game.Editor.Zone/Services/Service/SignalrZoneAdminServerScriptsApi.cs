namespace EventHorizon.Game.Editor.Zone.Services.Service
{
    using System.Threading;
    using System.Threading.Tasks;

    using EventHorizon.Game.Editor.Zone.Services.Api;
    using EventHorizon.Game.Editor.Zone.Services.Model;
    using EventHorizon.Zone.System.Server.Scripts.Model;

    using Microsoft.AspNetCore.SignalR.Client;

    public class SignalrZoneAdminServerScriptsApi
        : ZoneAdminServerScriptsApi
    {
        private readonly HubConnection? _hubConnection;

        internal SignalrZoneAdminServerScriptsApi(
            HubConnection? hubConnection
        )
        {
            _hubConnection = hubConnection;

        }

        public async Task<ApiResponse<ServerScriptsErrorDetailsResponse>> GetErrorDetails(
            CancellationToken cancellationToken
        )
        {
            if (_hubConnection.IsNotConnected())
            {
                return new ApiResponse<ServerScriptsErrorDetailsResponse>()
                {
                    Success = false,
                    ErrorCode = ZoneAdminErrorCodes.NOT_CONNECTED,
                };
            }

            return await _hubConnection.InvokeAsync<ApiResponse<ServerScriptsErrorDetailsResponse>>(
                "ServerScripts_ErrorDetails",
                cancellationToken
            );
        }
    }
}
