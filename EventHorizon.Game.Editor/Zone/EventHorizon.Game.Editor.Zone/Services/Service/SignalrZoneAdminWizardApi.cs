namespace EventHorizon.Game.Editor.Zone.Services.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Zone.Services.Api;
    using EventHorizon.Game.Editor.Zone.Services.Model;
    using EventHorizon.Zone.Systems.Wizard.Model;
    using Microsoft.AspNetCore.SignalR.Client;

    public sealed class SignalrZoneAdminWizardApi
        : ZoneAdminWizardApi
    {
        private readonly HubConnection? _hubConnection;

        internal SignalrZoneAdminWizardApi(
            HubConnection? hubConnection
        )
        {
            _hubConnection = hubConnection;

        }

        public Task<ApiResponse<List<WizardMetadata>>> All()
        {
            if (_hubConnection.IsNull())
            {
                return new ApiResponse<List<WizardMetadata>>()
                {
                    Success = false,
                    ErrorCode = ZoneAdminErrorCodes.NOT_CONNECTED,
                }.FromResult();
            }

            return _hubConnection.InvokeAsync<ApiResponse<List<WizardMetadata>>>(
                "Wizard_All"
            );
        }
    }
}
