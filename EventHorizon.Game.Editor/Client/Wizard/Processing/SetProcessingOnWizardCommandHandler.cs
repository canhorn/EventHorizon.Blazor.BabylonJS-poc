namespace EventHorizon.Game.Editor.Client.Wizard.Processing;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.Wizard.Api;
using MediatR;

public class SetProcessingOnWizardCommandHandler
    : IRequestHandler<SetProcessingOnWizardCommand, StandardCommandResult>
{
    private readonly WizardState _state;

    public SetProcessingOnWizardCommandHandler(WizardState state)
    {
        _state = state;
    }

    public async Task<StandardCommandResult> Handle(
        SetProcessingOnWizardCommand request,
        CancellationToken cancellationToken
    )
    {
        return await _state.IsProcessing(request.Context, request.IsProcessing);
    }
}
