namespace EventHorizon.Game.Editor.Client.Zone.Components.FileExplorer.Modals
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Client.Localization;
    using EventHorizon.Game.Editor.Client.Localization.Api;
    using EventHorizon.Game.Editor.Client.Zone.Components.FileExplorer.Model;
    using Microsoft.AspNetCore.Components;

    public class EditorFileExplorerModalModel
        : ComponentBase
    {
        [Inject]
        public Localizer<SharedResource> Localizer { get; set; } = null!;

        [Parameter]
        public EditorFileExplorerModalState ModalState { get; set; } = null!;
        [Parameter]
        public EventCallback OnSubmit { get; set; }
        [Parameter]
        public EventCallback OnClose { get; set; }

        protected override async Task OnAfterRenderAsync(
            bool firstRender
        )
        {
            if (firstRender)
            {
                if (ModalState.TriggerInputFocus)
                {
                    await ModalState.InputFocusElement.FocusAsync();
                }
                else if (ModalState.TriggerButtonFocus)
                {
                    await ModalState.ButtonFocusElement.FocusAsync();
                }
            }
        }
    }
}
