namespace EventHorizon.Game.Editor.Client.GraphEditor.Components;

using System.Reflection;
using Microsoft.AspNetCore.Components;

public abstract class BasePropertyDrawer : ComponentBase
{
    [Parameter]
    public required object Instance { get; set; }

    [Parameter]
    public required PropertyInfo Property { get; set; }
}
