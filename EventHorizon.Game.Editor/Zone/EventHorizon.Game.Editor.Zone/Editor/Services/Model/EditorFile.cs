namespace EventHorizon.Game.Editor.Zone.Editor.Services.Model;

using System.Collections.Generic;

public class EditorFile
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public IList<string> Path { get; set; } = new List<string>();
    public string Content { get; set; } = string.Empty;
}
