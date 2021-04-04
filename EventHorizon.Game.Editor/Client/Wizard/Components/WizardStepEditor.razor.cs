namespace EventHorizon.Game.Editor.Client.Wizard.Components
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Client.Localization;
    using EventHorizon.Game.Editor.Client.Localization.Api;
    using EventHorizon.Game.Editor.Client.Wizard.Api;
    using EventHorizon.Zone.Systems.Wizard.Model;
    using Microsoft.AspNetCore.Components;

    public class WizardStepEditorModel
        : ComponentBase
    {
        [CascadingParameter]
        public WizardState State { get; set; } = null!;

        [Parameter]
        public WizardStep Step { get; set; } = null!;
        [Parameter]
        public WizardData Data { get; set; } = null!;

        [Inject]
        public Localizer<SharedResource> Localizer { get; set; } = null!;

        public string ErrorMessage { get; set; } = string.Empty;

        public async Task HandlePreviousClicked()
        {
            ErrorMessage = string.Empty;
            var result = await State.Previous();
            if (!result)
            {
                ErrorMessage = Localizer[
                    "Failed to Go Back: {0}",
                    result.ErrorCode
                ];
            }
        }

        public bool IsPreviousDisabled()
        {
            return Step.IsProcessing
                || !Step.HasPrevious
                || Data[$"DisablePrevious:{Step.Id}"] == "true";
        }

        public async Task HandleNextClicked()
        {
            ErrorMessage = string.Empty;
            var result = await State.Next();
            if (!result)
            {
                ErrorMessage = Localizer[
                    "Failed to Proceed: {0}",
                    result.ErrorCode
                ];
            }
        }

        public bool IsNextDisabled()
        {
            return Step.IsInvalid
                || Step.IsProcessing
                || !Step.HasNext;
        }

        public async Task HandleCancelClicked()
        {
            ErrorMessage = string.Empty;
            var result = await State.Cancel();
            if (!result)
            {
                ErrorMessage = Localizer[
                    "Failed to Proceed: {0}",
                    result.ErrorCode
                ];
            }
        }

        public bool IsCancelDisabled()
        {
            return Step.IsProcessing;
        }
    }
}
