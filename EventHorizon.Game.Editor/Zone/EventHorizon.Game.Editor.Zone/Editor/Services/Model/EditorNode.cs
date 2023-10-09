namespace EventHorizon.Game.Editor.Zone.Editor.Services.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public class EditorNode
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool IsFolder { get; set; }
    public IList<string> Path { get; set; } = new List<string>();
    public string Type { get; set; } = string.Empty;
    public IList<EditorNode> Children { get; set; } = new List<EditorNode>();
    public EditorNodeTypedProperties Properties { get; set; } =
        new EditorNodeTypedProperties();
}

public class EditorNodeTypedProperties
{
    [JsonPropertyName("support:contextMenu")]
    public bool? SupportContextMenu { get; set; }

    [JsonPropertyName("support:delete")]
    public bool? SupportDelete { get; set; }

    [JsonPropertyName("language")]
    public string Language { get; set; } = string.Empty;
}
