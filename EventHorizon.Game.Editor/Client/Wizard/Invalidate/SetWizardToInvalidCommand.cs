namespace EventHorizon.Game.Editor.Client.Wizard.Invalidate;

using EventHorizon.Game.Client.Core.Command.Model;
using MediatR;

public struct SetWizardToInvalidCommand : IRequest<StandardCommandResult>
{
    public string ErrorCode { get; }

    public SetWizardToInvalidCommand(string errorCode)
    {
        ErrorCode = errorCode;
    }
}
