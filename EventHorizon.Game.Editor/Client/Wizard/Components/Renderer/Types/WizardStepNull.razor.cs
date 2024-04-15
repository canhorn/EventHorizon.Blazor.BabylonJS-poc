namespace EventHorizon.Game.Editor.Client.Wizard.Components.Renderer.Types;

using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Wizard.Api;
using EventHorizon.Zone.Systems.Wizard.Model;
using Microsoft.AspNetCore.Components;

public class WizardStepNullBase : EditorComponentBase
{
    [CascadingParameter]
    public WizardState State { get; set; } = null!;

    [Parameter]
    public WizardStep Step { get; set; } = null!;

    [Parameter]
    public WizardData Data { get; set; } = null!;
}
