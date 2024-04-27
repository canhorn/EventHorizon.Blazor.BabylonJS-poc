namespace EventHorizon.Game.Editor.Client.Wizard.Components.Provider;

using EventHorizon.Game.Editor.Client.Shared.Components;
using Microsoft.AspNetCore.Components;

public record WizardContextState(string Context);

public class WizardContextProviderBase : EditorComponentBase
{
    [Parameter]
    public required string Context { get; set; }

    [Parameter]
    public required RenderFragment ChildContent { get; set; }

    protected WizardContextState State { get; private set; } = new("not_set");

    protected override void OnInitialized()
    {
        base.OnInitialized();

        State = new WizardContextState(Context);
    }
}
