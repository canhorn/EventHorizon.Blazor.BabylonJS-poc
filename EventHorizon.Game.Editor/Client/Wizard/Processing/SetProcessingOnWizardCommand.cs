namespace EventHorizon.Game.Editor.Client.Wizard.Processing;

using EventHorizon.Game.Client.Core.Command.Model;
using MediatR;

public record SetProcessingOnWizardCommand(string Context, bool IsProcessing)
    : IRequest<StandardCommandResult>;
