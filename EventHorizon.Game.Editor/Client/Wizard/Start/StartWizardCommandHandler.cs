namespace EventHorizon.Game.Editor.Client.Wizard.Start;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.Wizard.Api;
using MediatR;

public class StartWizardCommandHandler : IRequestHandler<StartWizardCommand, StandardCommandResult>
{
    private readonly WizardState _state;

    public StartWizardCommandHandler(WizardState state)
    {
        _state = state;
    }

    public async Task<StandardCommandResult> Handle(
        StartWizardCommand request,
        CancellationToken cancellationToken
    )
    {
        return await _state.Start(request.Wizard);
    }
}
