namespace EventHorizon.Game.Editor.Client.Wizard.Previous;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.Wizard.Api;

using MediatR;

public class GoToPreviousWizardStepCommandHandler
    : IRequestHandler<GoToPreviousWizardStepCommand, StandardCommandResult>
{
    private readonly WizardState _state;

    public GoToPreviousWizardStepCommandHandler(WizardState state)
    {
        _state = state;
    }

    public async Task<StandardCommandResult> Handle(
        GoToPreviousWizardStepCommand request,
        CancellationToken cancellationToken
    )
    {
        return await _state.Previous();
    }
}
