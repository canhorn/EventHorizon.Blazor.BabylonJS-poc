namespace EventHorizon.Game.Editor.Zone.Editor.Services.Model
{
    using System.Collections.Generic;

    public class EditorFile
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IList<string> Path { get; set; }
        public string Content { get; set; }
    }
}
