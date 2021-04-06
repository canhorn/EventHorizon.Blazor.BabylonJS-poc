namespace EventHorizon.Game.Editor.Client.Wizard.Update
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Client.Wizard.Api;
    using MediatR;

    public class UpdateWizardDataCommandHandler
        : IRequestHandler<UpdateWizardDataCommand, StandardCommandResult>
    {
        private readonly WizardState _state;

        public UpdateWizardDataCommandHandler(
            WizardState state
        )
        {
            _state = state;
        }

        public async Task<StandardCommandResult> Handle(
            UpdateWizardDataCommand request,
            CancellationToken cancellationToken
        )
        {
            return await _state.UpdateData(
                request.WizardData
            );
        }
    }
}
