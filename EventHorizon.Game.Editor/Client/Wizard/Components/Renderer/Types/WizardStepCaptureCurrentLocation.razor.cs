namespace EventHorizon.Game.Editor.Client.Wizard.Components.Renderer.Types;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

public class WizardStepCaptureCurrentLocationBase : WizardStepCommonBase
{
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    public string ErrorMessage { get; set; } = string.Empty;

    protected override async Task OnInitializingAsync()
    {
        var currentLocation = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);

        if (Step.Details.TryGetValue("CurrentLocationProperty", out var property))
        {
            Data[property] = currentLocation;

            await State.Next();
            return;
        }

        Data["CurrentLocation"] = currentLocation;

        await State.Next();
    }
}
