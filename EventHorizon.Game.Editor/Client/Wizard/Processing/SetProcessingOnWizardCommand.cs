namespace EventHorizon.Game.Editor.Client.Wizard.Processing;

using EventHorizon.Game.Client.Core.Command.Model;
using MediatR;

public struct SetProcessingOnWizardCommand : IRequest<StandardCommandResult>
{
    public bool IsProcessing { get; }

    public SetProcessingOnWizardCommand(bool isProcessing)
    {
        IsProcessing = isProcessing;
    }
}
