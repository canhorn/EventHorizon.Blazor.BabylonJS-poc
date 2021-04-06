namespace EventHorizon.Game.Editor.Client.Wizard.Next
{
    using EventHorizon.Game.Client.Core.Command.Model;
    using MediatR;

    public struct GoToNextWizardStepCommand
        : IRequest<StandardCommandResult>
    {
    }
}
