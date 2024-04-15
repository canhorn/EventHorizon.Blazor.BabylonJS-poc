namespace EventHorizon.Game.Editor.Client.Shared.Properties;

using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Zone.Api;
using Microsoft.AspNetCore.Components;

public class PropertyDisplayModel : EditorComponentBase
{
    [CascadingParameter]
    public ZoneState State { get; set; } = null!;

    [Parameter]
    public PropertyDisplayType Property { get; set; } = null!;

    [Parameter]
    public EventCallback<PropertyChangedArgs> OnChanged { get; set; }

    [Parameter]
    public EventCallback<string> OnRemove { get; set; }
}
