namespace EventHorizon.Game.Editor.Client.PlayerEditor.Components;

using System;
using System.Linq;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Wizard.Api;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

public class PlayerConfigurationEditorComponentBase : EditorComponentBase, IDisposable
{
    [CascadingParameter]
    public required WizardState State { get; set; }

    [Inject]
    public required ILogger<PlayerConfigurationEditorComponentBase> Logger { get; set; }

    public bool IsCurrentWizard { get; set; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        if (
            !State.CurrentStep
            && State.WizardList.Any(a => a.Id == "1b424a53-5a93-43d0-8f53-ed240beb3071")
        )
        {
            IsCurrentWizard = true;
            State.Start(
                State.WizardList.First(a => a.Id == "1b424a53-5a93-43d0-8f53-ed240beb3071"),
                true
            );
        }
        else if (State.CurrentStep)
        {
            IsCurrentWizard = State.CurrentWizardId == "1b424a53-5a93-43d0-8f53-ed240beb3071";
        }
    }

    protected void HandleStartEditing()
    {
        State.Start(
            State.WizardList.First(a => a.Id == "1b424a53-5a93-43d0-8f53-ed240beb3071"),
            true
        );
        IsCurrentWizard = true;
    }

    public void Dispose()
    {
        Console.WriteLine("Disposing PlayerConfigurationEditorComponentBase");
        State.Cancel();
    }
}
