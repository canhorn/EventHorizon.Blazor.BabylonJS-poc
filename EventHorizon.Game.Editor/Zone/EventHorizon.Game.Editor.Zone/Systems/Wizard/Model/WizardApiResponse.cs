namespace EventHorizon.Game.Editor.Zone.Systems.Wizard.Model;

using EventHorizon.Game.Editor.Zone.Services.Model;
using EventHorizon.Zone.Systems.Wizard.Model;

public class WizardApiResponse : StandardApiResponse
{
    public WizardData Result { get; set; } = new WizardData();
}
