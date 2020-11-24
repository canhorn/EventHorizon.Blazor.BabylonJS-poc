namespace EventHorizon.Game.Editor.Client.Shared.Components.TreeView
{
    using EventHorizon.Game.Editor.Client.Shared.Components.TreeView.Model;
    using Microsoft.AspNetCore.Components;

    public class TreeViewModel : ComponentBase
    {
        [Parameter]
        public TreeViewNodeData Root { get; set; }
    }
}
