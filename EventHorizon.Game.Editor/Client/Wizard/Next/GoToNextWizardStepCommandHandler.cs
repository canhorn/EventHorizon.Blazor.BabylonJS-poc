namespace EventHorizon.Game.Editor.Client.Wizard.Next;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.Wizard.Api;
using MediatR;

public class GoToNextWizardStepCommandHandler
    : IRequestHandler<GoToNextWizardStepCommand, StandardCommandResult>
{
    private readonly WizardState _state;

    public GoToNextWizardStepCommandHandler(WizardState state)
    {
        _state = state;
    }

    public async Task<StandardCommandResult> Handle(
        GoToNextWizardStepCommand request,
        CancellationToken cancellationToken
    )
    {
        return await _state.Next();
    }
}
