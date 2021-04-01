namespace EventHorizon.Game.Editor.Zone.Services.Api
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Zone.Services.Model;
    using EventHorizon.Zone.Systems.Wizard.Model;

    public interface ZoneAdminWizardApi
    {
        Task<ApiResponse<List<WizardMetadata>>> All(
            CancellationToken cancellationToken
        );

        Task<StandardApiResponse> RunScriptProcessor(
            string wizardId,
            string wizardStepId,
            string processorScriptId,
            WizardData wizardData,
            CancellationToken cancellationToken
        );
    }
}
