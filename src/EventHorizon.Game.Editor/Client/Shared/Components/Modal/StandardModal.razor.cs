namespace EventHorizon.Game.Editor.Client.Shared.Components.Modal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Client.Shared.ClickCapture;
    using EventHorizon.Game.Editor.Client.Shared.Components.Modal.Model;
    using Microsoft.AspNetCore.Components;

    public class StandardModalModel
        : ComponentBase
    {
        [CascadingParameter]
        public ClickCaptureProvider ClickCapture { get; set; }

        [Parameter]
        public string Theme { get; set; } = string.Empty;
        [Parameter]
        public bool IsOpen { get; set; }
        [Parameter]
        public ModalType Type { get; set; }
        [Parameter]
        public EventCallback OnClose { get; set; }
        [Parameter]
        public RenderFragment Header { get; set; }
        [Parameter]
        public RenderFragment Body { get; set; }
        [Parameter]
        public RenderFragment Footer { get; set; }

        public string SizeCss { get; set; } = string.Empty;

        protected override void OnInitialized()
        {
            SizeCss = GetSizeCss();
        }

        protected override void OnParametersSet()
        {
            SizeCss = GetSizeCss();
        }

        protected async Task HandleMouseClick()
        {
            await OnClose.InvokeAsync(null);
        }

        private string GetSizeCss()
        {
            return Type switch
            {
                ModalType.FullScreen => "--full",
                ModalType.Slim => "--slim",
                _ => string.Empty,
            };
        }
    }
}
