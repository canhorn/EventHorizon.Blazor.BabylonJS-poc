namespace EventHorizon.Game.Editor.Client.Wizard.Components.Renderer.Types
{
    using Microsoft.AspNetCore.Components;

    public class WizardStepNavigateBase
        : WizardStepCommonBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        public string ErrorMessage { get; set; } = string.Empty;

        protected override void OnInitializing()
        {
            var locationFound = Step.Details.TryGetValue(
                "Location",
                out var location
            );
            if (!locationFound)
            {
                ErrorMessage = Localizer["Navigation Location was not found. Check the above description for more details."];
                return;
            }

            NavigationManager.NavigateTo(
                location!
            );
        }
    }
}
