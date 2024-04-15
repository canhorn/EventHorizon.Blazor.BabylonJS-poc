namespace EventHorizon.Game.Editor.Client.Wizard.Invalidate;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.Wizard.Api;
using MediatR;

public class SetWizardToInvalidCommandHandler
    : IRequestHandler<SetWizardToInvalidCommand, StandardCommandResult>
{
    private readonly WizardState _state;

    public SetWizardToInvalidCommandHandler(WizardState state)
    {
        _state = state;
    }

    public async Task<StandardCommandResult> Handle(
        SetWizardToInvalidCommand request,
        CancellationToken cancellationToken
    )
    {
        return await _state.SetToInvalid(request.ErrorCode);
    }
}
