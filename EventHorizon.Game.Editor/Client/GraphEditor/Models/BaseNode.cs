namespace EventHorizon.Game.Editor.Client.GraphEditor.Models;

public class BaseNode
{
    public required string Id;
    public string Name = string.Empty;
    public string Description = string.Empty;
    public bool Collapsed = false;
    public Point Position;
}
