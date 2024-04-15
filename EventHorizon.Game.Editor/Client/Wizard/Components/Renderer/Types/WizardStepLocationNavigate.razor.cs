namespace EventHorizon.Game.Editor.Client.Wizard.Components.Renderer.Types;

using Microsoft.AspNetCore.Components;

public class WizardStepLocationNavigateBase : WizardStepCommonBase
{
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    public string ErrorMessage { get; set; } = string.Empty;

    protected override void OnInitializing()
    {
        if (!Step.Details.TryGetValue("LocationProperty", out var locationProperty))
        {
            ErrorMessage = Localizer["Location Property was not found."];
            return;
        }

        if (!Data.TryGetValue(locationProperty, out var location))
        {
            ErrorMessage = Localizer["Location Property was not found."];
            return;
        }

        NavigationManager.NavigateTo(location);
    }
}
