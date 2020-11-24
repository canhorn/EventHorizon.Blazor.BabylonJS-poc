namespace EventHorizon.Game.Editor.Client.Shared.Components.TreeView.Model
{
    using System.Collections.Generic;

    public class TreeViewNodeData
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string Href { get; set; } = string.Empty;
        public string IconCssClass { get; set; } = string.Empty;
        public bool IsExpanded { get; set; }
        public bool IsDisabled { get; set; }
        public IList<TreeViewNodeData> Children { get; set; } = new List<TreeViewNodeData>();
        public TreeViewNodeContextMenu ContextMenu { get; set; } = new TreeViewNodeContextMenu();
    }
}
