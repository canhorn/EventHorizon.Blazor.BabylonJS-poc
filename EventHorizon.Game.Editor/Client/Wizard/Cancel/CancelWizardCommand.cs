namespace EventHorizon.Game.Editor.Client.Wizard.Cancel;

using EventHorizon.Game.Client.Core.Command.Model;
using MediatR;

public record CancelWizardCommand(string Context) : IRequest<StandardCommandResult>;
