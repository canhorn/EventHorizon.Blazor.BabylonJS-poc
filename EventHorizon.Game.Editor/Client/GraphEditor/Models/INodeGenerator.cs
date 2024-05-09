namespace EventHorizon.Game.Editor.Client.GraphEditor.Models;

public interface INodeGenerator<NodeType>
{
    string GeneratorName();
    NodeType Generate();
}
