namespace EventHorizon.Game.Editor.Client.Wizard.Start;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Zone.Systems.Wizard.Model;

using MediatR;

public struct StartWizardCommand : IRequest<StandardCommandResult>
{
    public WizardMetadata Wizard { get; }

    public StartWizardCommand(WizardMetadata wizard)
    {
        Wizard = wizard;
    }
}
