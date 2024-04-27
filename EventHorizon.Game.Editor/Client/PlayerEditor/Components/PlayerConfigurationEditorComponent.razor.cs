namespace EventHorizon.Game.Editor.Client.PlayerEditor.Components;

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Wizard.Api;
using EventHorizon.Game.Editor.Client.Wizard.Model;
using Microsoft.AspNetCore.Components;

public class PlayerConfigurationEditorComponentBase : EditorComponentBase, IDisposable
{
    private const string PlayerConfigWizardId = "1b424a53-5a93-43d0-8f53-ed240beb3071";

    [CascadingParameter]
    public required WizardState State { get; set; }

    public bool IsCurrentWizard { get; set; }
    protected string WizardContext = PlayerConfigWizardId;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (
            !State.CurrentStep(WizardContext)
            && State.WizardList.Any(a => a.Id == PlayerConfigWizardId)
        )
        {
            State.Start(WizardContext, State.WizardList.First(a => a.Id == PlayerConfigWizardId));
        }

        State.OnChange += HandleStateChanged;
    }

    private async Task HandleStateChanged(WizardStateChangeArgs args)
    {
        if (
            args.Reason == WizardChangeReasons.WIZARD_LIST_UPDATED
            && !State.CurrentStep(WizardContext)
        )
        {
            await State.Start(
                WizardContext,
                State.WizardList.First(a => a.Id == PlayerConfigWizardId)
            );
        }
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        IsCurrentWizard = State.CurrentWizardId(WizardContext) == PlayerConfigWizardId;

        if (!IsCurrentWizard && State.WizardList.Any(a => a.Id == PlayerConfigWizardId))
        {
            await State.Start(
                WizardContext,
                State.WizardList.First(a => a.Id == PlayerConfigWizardId)
            );
        }
    }

    protected void HandleStartEditing()
    {
        State.Start(WizardContext, State.WizardList.First(a => a.Id == PlayerConfigWizardId));
    }

    public void Dispose()
    {
        State.OnChange -= HandleStateChanged;

        if (IsCurrentWizard)
        {
            State.Cancel(WizardContext);
        }
    }
}
