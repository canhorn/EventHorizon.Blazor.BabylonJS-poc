namespace EventHorizon.Game.Editor.Client.Wizard.Components.Renderer.Types;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

public class WizardStepTextInputBase : WizardStepCommonBase
{
    protected ElementReference TextInputElement { get; set; }

    protected string TextValue
    {
        get
        {
            if (Step.Details.TryGetValue("property", out var property))
            {
                return Data[property];
            }
            return string.Empty;
        }
        set
        {
            if (Step.Details.TryGetValue("property", out var property))
            {
                Data[property] = value;
            }

            InvokeAsync(() => State.UpdateData(ContextState.Context, Data));
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await TextInputElement.FocusAsync();
        }
    }
}
