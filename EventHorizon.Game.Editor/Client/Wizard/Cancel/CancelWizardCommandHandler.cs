namespace EventHorizon.Game.Editor.Client.Wizard.Cancel;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.Wizard.Api;

using MediatR;

public class CancelWizardCommandHandler
    : IRequestHandler<CancelWizardCommand, StandardCommandResult>
{
    private readonly WizardState _state;

    public CancelWizardCommandHandler(WizardState state)
    {
        _state = state;
    }

    public async Task<StandardCommandResult> Handle(
        CancelWizardCommand request,
        CancellationToken cancellationToken
    )
    {
        return await _state.Cancel();
    }
}
