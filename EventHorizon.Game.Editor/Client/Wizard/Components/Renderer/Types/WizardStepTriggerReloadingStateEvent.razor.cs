namespace EventHorizon.Game.Editor.Client.Wizard.Components.Renderer.Types;

using System.Threading.Tasks;
using EventHorizon.Game.Editor.Client.Zone.Reload;

public class WizardStepTriggerReloadingStateEventBase : WizardStepCommonBase
{
    protected override async Task OnInitializingAsync()
    {
        await Mediator.Send(new SetReloadOnZoneStateCommand(true));
    }
}
