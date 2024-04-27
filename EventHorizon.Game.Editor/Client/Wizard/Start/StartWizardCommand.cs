namespace EventHorizon.Game.Editor.Client.Wizard.Start;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Zone.Systems.Wizard.Model;
using MediatR;

public record StartWizardCommand(string Context, WizardMetadata Wizard)
    : IRequest<StandardCommandResult>;
