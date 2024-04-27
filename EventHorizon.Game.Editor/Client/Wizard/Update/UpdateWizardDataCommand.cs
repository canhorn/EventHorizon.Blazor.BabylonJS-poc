namespace EventHorizon.Game.Editor.Client.Wizard.Update;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Zone.Systems.Wizard.Model;
using MediatR;

public record UpdateWizardDataCommand(string Context, WizardData WizardData)
    : IRequest<StandardCommandResult>;
