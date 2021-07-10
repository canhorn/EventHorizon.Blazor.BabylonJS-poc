namespace EventHorizon.Game.Editor.Client.Shared.Components.TreeView.Model
{
    using System;
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
        public object? Data { get; set; }

        #region Generated Equals/GetHashCode
        public override bool Equals(object? obj)
        {
            return obj is TreeViewNodeData data &&
                   Id == data.Id &&
                   Name == data.Name;
        }

        public override int GetHashCode()
        {
            HashCode hash = new();
            hash.Add(Id);
            hash.Add(Name);
            return hash.ToHashCode();
        }

        public static bool operator ==(TreeViewNodeData? left, TreeViewNodeData? right)
        {
            return EqualityComparer<TreeViewNodeData>.Default.Equals(left, right);
        }

        public static bool operator !=(TreeViewNodeData? left, TreeViewNodeData? right)
        {
            return !(left == right);
        }
        #endregion
    }
}
