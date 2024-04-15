namespace EventHorizon.Game.Editor.Client.Zone.Components.FileExplorer.Modals;

using System.Threading.Tasks;
using EventHorizon.Game.Editor.Client.Localization;
using EventHorizon.Game.Editor.Client.Localization.Api;
using EventHorizon.Game.Editor.Client.Zone.Components.FileExplorer.Model;
using Microsoft.AspNetCore.Components;

public class EditorFileExplorerModalModel : ComponentBase
{
    [Inject]
    public Localizer<SharedResource> Localizer { get; set; } = null!;

    [Parameter]
    public EditorFileExplorerModalState ModalState { get; set; } = null!;

    [Parameter]
    public EventCallback OnSubmit { get; set; }

    [Parameter]
    public EventCallback OnClose { get; set; }

    private bool _triggerFocus;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!ModalState.IsOpen)
        {
            return;
        }
        else if (!_triggerFocus && ModalState.TriggerInputFocus)
        {
            await ModalState.InputFocusElement.FocusAsync();
            _triggerFocus = true;
        }
        else if (!_triggerFocus && ModalState.TriggerButtonFocus)
        {
            await ModalState.ButtonFocusElement.FocusAsync();
            _triggerFocus = true;
        }
    }

    public void HandleModalClosed()
    {
        ModalState.HandleModalClosed();
        _triggerFocus = false;
    }
}
