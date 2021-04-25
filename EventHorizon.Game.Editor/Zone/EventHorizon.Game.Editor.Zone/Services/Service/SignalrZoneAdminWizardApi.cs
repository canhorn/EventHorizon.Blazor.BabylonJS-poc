namespace EventHorizon.Game.Editor.Zone.Services.Service
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Zone.Services.Api;
    using EventHorizon.Game.Editor.Zone.Services.Model;
    using EventHorizon.Game.Editor.Zone.Systems.Wizard.Model;
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

        public async Task<ApiResponse<List<WizardMetadata>>> All(
            CancellationToken cancellationToken
        )
        {
            if (_hubConnection.IsNull())
            {
                return new ApiResponse<List<WizardMetadata>>()
                {
                    Success = false,
                    ErrorCode = ZoneAdminErrorCodes.NOT_CONNECTED,
                };
            }

            return await _hubConnection.InvokeAsync<ApiResponse<List<WizardMetadata>>>(
                "Wizard_All",
                cancellationToken
            );
        }

        public async Task<WizardApiResponse> RunScriptProcessor(
            string wizardId,
            string wizardStepId,
            string processorScriptId,
            WizardData wizardData,
            CancellationToken cancellationToken
        )
        {
            if (_hubConnection.IsNull())
            {
                return new WizardApiResponse()
                {
                    Success = false,
                    ErrorCode = ZoneAdminErrorCodes.NOT_CONNECTED,
                };
            }

            return await _hubConnection.InvokeAsync<WizardApiResponse>(
                "Wizard_RunScriptProcessor",
                wizardId,
                wizardStepId,
                processorScriptId,
                wizardData,
                cancellationToken
            );
        }
    }
}
