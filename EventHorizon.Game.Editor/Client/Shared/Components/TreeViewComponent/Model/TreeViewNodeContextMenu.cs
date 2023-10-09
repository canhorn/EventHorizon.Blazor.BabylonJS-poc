namespace EventHorizon.Game.Editor.Client.Shared.Components.TreeViewComponent.Model;

using System;
using System.Collections.Generic;

public class TreeViewNodeContextMenu
{
    public IList<TreeViewNodeContextMenuItem> Items { get; set; } =
        new List<TreeViewNodeContextMenuItem>();
}

public class TreeViewNodeContextMenuItem
{
    public string Text { get; set; } = string.Empty;
    public Action OnClick { get; set; } = () => { };
}
