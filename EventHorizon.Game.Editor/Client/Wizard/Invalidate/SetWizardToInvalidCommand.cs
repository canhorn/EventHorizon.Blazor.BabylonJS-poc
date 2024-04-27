namespace EventHorizon.Game.Editor.Client.Wizard.Invalidate;

using EventHorizon.Game.Client.Core.Command.Model;
using MediatR;

public record SetWizardToInvalidCommand(string Context, string ErrorCode)
    : IRequest<StandardCommandResult>;
