namespace EventHorizon.Game.Editor.Zone.Editor.Services.Model
{
    using System.Collections.Generic;

    public class EditorNodeList
    {
        public IList<EditorNode> Root { get; set; } = new List<EditorNode>();
    }
}
