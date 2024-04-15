namespace EventHorizon.Game.Editor.Client.Shared.Components.TreeViewComponent;

using System.Collections.Generic;
using EventHorizon.Game.Editor.Client.Shared.Components.TreeViewComponent.Model;
using Microsoft.AspNetCore.Components;

public class TreeViewModel : ComponentBase
{
    [Parameter]
    public TreeViewNodeData Root { get; set; } = null!;

    [Parameter]
    public EventCallback<TreeViewNodeData> OnChanged { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object> Attributes { get; set; } = null!;
}
