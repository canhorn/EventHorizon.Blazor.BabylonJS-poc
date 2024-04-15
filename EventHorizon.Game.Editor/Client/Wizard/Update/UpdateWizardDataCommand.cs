namespace EventHorizon.Game.Editor.Client.Wizard.Update;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Zone.Systems.Wizard.Model;
using MediatR;

public struct UpdateWizardDataCommand : IRequest<StandardCommandResult>
{
    public WizardData WizardData { get; }

    public UpdateWizardDataCommand(WizardData wizardData)
    {
        WizardData = wizardData;
    }
}
