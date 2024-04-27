namespace EventHorizon.Game.Editor.Client.Wizard.Next;

using EventHorizon.Game.Client.Core.Command.Model;
using MediatR;

public record GoToNextWizardStepCommand(string Context) : IRequest<StandardCommandResult>;
