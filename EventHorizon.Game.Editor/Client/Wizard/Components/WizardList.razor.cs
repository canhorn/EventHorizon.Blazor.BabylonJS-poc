namespace EventHorizon.Game.Editor.Client.Wizard.Components;

using System;
using System.Threading.Tasks;
using EventHorizon.Game.Editor.Client.Localization;
using EventHorizon.Game.Editor.Client.Localization.Api;
using EventHorizon.Game.Editor.Client.Wizard.Api;
using EventHorizon.Game.Editor.Client.Wizard.Components.Provider;
using EventHorizon.Game.Editor.Client.Wizard.Start;
using EventHorizon.Zone.Systems.Wizard.Model;
using MediatR;
using Microsoft.AspNetCore.Components;

public class WizardListBase : ComponentBase
{
    [CascadingParameter]
    public WizardState State { get; set; } = null!;

    [CascadingParameter]
    public WizardContextState ContextState { get; set; } = null!;

    [Inject]
    public Localizer<SharedResource> Localizer { get; set; } = null!;

    [Inject]
    public IMediator Mediator { get; set; } = null!;

    public string ErrorMessage { get; set; } = string.Empty;
    public string WizardFilter { get; set; } = string.Empty;

    public async Task HandleWizardSelected(WizardMetadata wizard)
    {
        ErrorMessage = string.Empty;
        var result = await Mediator.Send(new StartWizardCommand(ContextState.Context, wizard));
        if (result.Success.IsNotTrue())
        {
            ErrorMessage = Localizer[
                "Failed to start Wizard ({0}) received error code of '{1}'",
                wizard.Id,
                result.ErrorCode
            ];
            return;
        }
    }

    protected bool FilterBySearchText(WizardMetadata wizardMetadata)
    {
        return WizardFilter == string.Empty
            || wizardMetadata.Id.Contains(WizardFilter, StringComparison.CurrentCultureIgnoreCase)
            || wizardMetadata.Name.Contains(WizardFilter, StringComparison.CurrentCultureIgnoreCase)
            || wizardMetadata.Description.Contains(
                WizardFilter,
                StringComparison.CurrentCultureIgnoreCase
            );
    }
}
