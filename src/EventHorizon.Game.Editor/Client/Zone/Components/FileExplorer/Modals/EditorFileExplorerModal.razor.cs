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
        public Localizer<SharedResource> Localizer { get; set; }
        //[Inject]
        //public JavaScriptRunner ScriptRunner { get; set; }

        [Parameter]
        public EditorFileExplorerModalState ModalState { get; set; }
        [Parameter]
        public EventCallback OnSubmit { get; set; }
        [Parameter]
        public EventCallback OnClose { get; set; }

        protected override async Task OnAfterRenderAsync(
            bool firstRender
        )
        {
            // TODO: Update Focus setting, use the new Blazor feature for Focus
            //if (ModalState.TriggerInputFocus)
            //{
            //    await ScriptRunner.Run(
            //        "zoneEditorExplorer.focusElement",
            //        "$args.element.focus();",
            //        new
            //        {
            //            element = ModalState.InputFocusElement
            //        }
            //    );
            //    ModalState.TriggerInputFocus = false;
            //}
            //else if (ModalState.TriggerButtonFocus)
            //{
            //    await ScriptRunner.Run(
            //        "zoneEditorExplorer.focusElement",
            //        "$args.element.focus();",
            //        new
            //        {
            //            element = ModalState.ButtonFocusElement
            //        }
            //    );
            //    ModalState.TriggerButtonFocus = false;
            //}
        }
    }
}
