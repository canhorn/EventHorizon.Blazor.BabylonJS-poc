namespace EventHorizon.Game.Editor.Zone.Editor.Services.Api
{
    using EventHorizon.Game.Editor.Zone.Editor.Services.Model;

    public interface ZoneEditorState
    {
        ZoneEditorMetadata Metadata { get; }
        EditorNodeList EditorNodeList { get; }
        EditorNode GetNode(
            string id
        );
    }
}
