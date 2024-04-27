namespace EventHorizon.Game.Editor.Client.Wizard.Components;

using System.Threading.Tasks;
using EventHorizon.Game.Editor.Client.Localization;
using EventHorizon.Game.Editor.Client.Localization.Api;
using EventHorizon.Game.Editor.Client.Wizard.Cancel;
using EventHorizon.Game.Editor.Client.Wizard.Components.Provider;
using EventHorizon.Game.Editor.Client.Wizard.Next;
using EventHorizon.Game.Editor.Client.Wizard.Previous;
using EventHorizon.Zone.Systems.Wizard.Model;
using MediatR;
using Microsoft.AspNetCore.Components;

public class WizardStepEditorModel : ComponentBase
{
    [CascadingParameter]
    public required WizardContextState ContextState { get; set; } = null!;

    [Parameter]
    public WizardStep Step { get; set; } = null!;

    [Parameter]
    public WizardData Data { get; set; } = null!;

    [Inject]
    public IMediator Mediator { get; set; } = null!;

    [Inject]
    public Localizer<SharedResource> Localizer { get; set; } = null!;

    public string ErrorMessage { get; set; } = string.Empty;

    public async Task HandlePreviousClicked()
    {
        ErrorMessage = string.Empty;
        var result = await Mediator.Send(new GoToPreviousWizardStepCommand(ContextState.Context));
        if (!result)
        {
            ErrorMessage = Localizer["Failed to Go Back: {0}", result.ErrorCode];
        }
    }

    public bool IsPreviousDisabled()
    {
        return Step.IsProcessing
            || !Step.HasPrevious
            || Step.Details["DisablePrevious"] == "true"
            || Data[$"DisablePrevious:{Step.Id}"] == "true";
    }

    public async Task HandleNextClicked()
    {
        ErrorMessage = string.Empty;
        var result = await Mediator.Send(new GoToNextWizardStepCommand(ContextState.Context));
        if (!result)
        {
            ErrorMessage = Localizer["Failed to Proceed: {0}", result.ErrorCode];
        }
    }

    public bool IsNextDisabled()
    {
        return Step.IsInvalid
            || Step.IsProcessing
            || Step.Details["DisableNext"] == "true"
            || !Step.HasNext;
    }

    public async Task HandleCancelClicked()
    {
        ErrorMessage = string.Empty;
        var result = await Mediator.Send(new CancelWizardCommand(ContextState.Context));
        if (!result)
        {
            ErrorMessage = Localizer["Failed to Proceed: {0}", result.ErrorCode];
        }
    }

    public bool IsCancelDisabled()
    {
        return Step.IsProcessing;
    }
}
