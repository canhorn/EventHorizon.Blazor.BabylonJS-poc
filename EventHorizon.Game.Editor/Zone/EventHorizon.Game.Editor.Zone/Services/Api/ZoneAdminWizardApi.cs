namespace EventHorizon.Game.Editor.Zone.Services.Api
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Zone.Services.Model;
    using EventHorizon.Zone.Systems.Wizard.Model;

    public interface ZoneAdminWizardApi
    {
        Task<ApiResponse<List<WizardMetadata>>> All();
    }
}
