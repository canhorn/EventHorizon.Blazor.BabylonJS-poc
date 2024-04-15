namespace EventHorizon.Game.Editor.Client.Shared.Components.Modal;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using EventHorizon.Game.Editor.Client.Localization;
using EventHorizon.Game.Editor.Client.Localization.Api;
using EventHorizon.Game.Editor.Client.Shared.ClickCapture;
using EventHorizon.Game.Editor.Client.Shared.Components.Modal.Model;
using Microsoft.AspNetCore.Components;

public class StandardModalModel : ComponentBase
{
    [CascadingParameter]
    public ClickCaptureProvider ClickCapture { get; set; } = null!;

    [Parameter]
    public string id { get; set; } = string.Empty;

    [Parameter]
    public string Theme { get; set; } = string.Empty;

    [Parameter]
    public bool IsOpen { get; set; }

    [Parameter]
    public ModalType Type { get; set; }

    [Parameter]
    public EventCallback OnClose { get; set; }

    [Parameter]
    public EventCallback OnShown { get; set; }

    [Parameter]
    [MaybeNull]
    public RenderFragment Header { get; set; }

    [Parameter]
    public RenderFragment Body { get; set; } = null!;

    [Parameter]
    [MaybeNull]
    public RenderFragment Footer { get; set; }

    [Parameter]
    public bool HideClose { get; set; }

    [Inject]
    public Localizer<SharedResource> Localizer { get; set; } = null!;

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
            ModalType.Fit => "--fit",
            _ => string.Empty,
        };
    }
}
