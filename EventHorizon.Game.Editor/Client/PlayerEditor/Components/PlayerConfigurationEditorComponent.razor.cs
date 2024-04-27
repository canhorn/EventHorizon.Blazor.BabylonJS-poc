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

    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (!State.CurrentStep && State.WizardList.Any(a => a.Id == PlayerConfigWizardId))
        {
            State.Start(State.WizardList.First(a => a.Id == PlayerConfigWizardId), true);
        }

        State.OnChange += HandleStateChanged;
    }

    private async Task HandleStateChanged(WizardStateChangeArgs args)
    {
        if (args.Reason == WizardChangeReasons.WIZARD_LIST_UPDATED && !State.CurrentStep)
        {
            await State.Start(State.WizardList.First(a => a.Id == PlayerConfigWizardId), true);
        }
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        IsCurrentWizard = State.CurrentWizardId == PlayerConfigWizardId;
    }

    protected void HandleStartEditing()
    {
        State.Start(State.WizardList.First(a => a.Id == PlayerConfigWizardId), true);
    }

    public void Dispose()
    {
        State.OnChange -= HandleStateChanged;

        if (IsCurrentWizard)
        {
            State.Cancel();
        }
    }
}
