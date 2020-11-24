namespace EventHorizon.Game.Editor.Client.Shared.Components.TreeView.Model
{
    using System;
    using System.Collections.Generic;

    public class TreeViewNodeContextMenu
    {
        public IList<TreeViewNodeContextMenuItem> Items { get; set; } = new List<TreeViewNodeContextMenuItem>();
    }
    public class TreeViewNodeContextMenuItem
    {
        public string Text { get; set; }
        public Action OnClick { get; set; }
    }
}
