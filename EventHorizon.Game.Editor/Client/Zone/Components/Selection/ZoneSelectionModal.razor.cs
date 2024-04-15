namespace EventHorizon.Game.Editor.Client.Zone.Components.Selection;

using System.Collections.Generic;
using System.Threading.Tasks;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Core.Services.Model;
using Microsoft.AspNetCore.Components;

public class ZoneSelectionModalBase : EditorComponentBase
{
    [Parameter]
    public bool IsOpen { get; set; }

    [Parameter]
    public bool ForceSelection { get; set; } = true;

    [Parameter]
    public IEnumerable<CoreZoneDetails> Zones { get; set; } = null!;

    [Parameter]
    public string SelectedZoneId { get; set; } = string.Empty;

    [Parameter]
    public EventCallback<string> OnZoneSelectionChanged { get; set; }

    [Parameter]
    public EventCallback OnClose { get; set; }

    protected async Task HandleZoneSelectionChanged(string zoneId)
    {
        await OnZoneSelectionChanged.InvokeAsync(zoneId);
    }

    protected async Task HandleCloseModal()
    {
        await OnClose.InvokeAsync();
    }
}
