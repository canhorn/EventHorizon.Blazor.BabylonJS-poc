namespace EventHorizon.Game.Editor.Client.Wizard.Components.Renderer.Types;

using System;
using System.Threading.Tasks;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Wizard.Api;
using EventHorizon.Zone.Systems.Wizard.Model;
using Microsoft.AspNetCore.Components;

public abstract class WizardStepCommonBase : ObservableComponentBase
{
    [CascadingParameter]
    public WizardState State { get; set; } = null!;

    [Parameter]
    public WizardStep Step { get; set; } = null!;

    [Parameter]
    public WizardData Data { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        if (
            Step.Details.TryGetValue("Delay", out var delay)
            && int.TryParse(delay, out var delayAsInt)
        )
        {
            await Task.Delay(delayAsInt);
        }

        OnInitializing();
        await OnInitializingAsync();

        if (
            Step.Details.TryGetValue("AutoNext", out var autoNext)
            && autoNext.Equals("true", StringComparison.InvariantCultureIgnoreCase)
        )
        {
            await State.Next();
        }
    }

    protected virtual void OnInitializing() { }

    protected virtual Task OnInitializingAsync() => Task.CompletedTask;
}
