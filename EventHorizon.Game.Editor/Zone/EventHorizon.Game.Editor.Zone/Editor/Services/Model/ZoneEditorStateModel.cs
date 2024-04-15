namespace EventHorizon.Game.Editor.Zone.Editor.Services.Model;

using System.Collections.Generic;
using EventHorizon.Game.Editor.Zone.Editor.Services.Api;

public class ZoneEditorStateModel : ZoneEditorState
{
    private IDictionary<string, EditorNode> _map = new Dictionary<string, EditorNode>();

    public EditorNodeList EditorNodeList { get; private set; } = new EditorNodeList();
    public ZoneEditorMetadata Metadata { get; private set; } =
        new ZoneEditorMetadata
        {
            // TODO: [PropertyMetadata]: Move this to the server for persistence
            ZoneEditorPropertyTypeMap = new Dictionary<string, string>
            {
                ["assetId"] = ZoneEditorPropertyType.PropertyAsset,
                ["dense"] = ZoneEditorPropertyType.PropertyBoolean,
                ["resolveHeight"] = ZoneEditorPropertyType.PropertyBoolean,
                ["heightOffset"] = ZoneEditorPropertyType.PropertyDecimal,
                ["densityBox"] = ZoneEditorPropertyType.PropertyVector3,
            },
        };

    public ZoneEditorStateModel(EditorNodeList editorNodeList)
    {
        SetInternalState(editorNodeList);
    }

    public ZoneEditorStateModel SetInternalState(EditorNodeList editorNodeList)
    {
        editorNodeList.NullCheck(nameof(editorNodeList));
        EditorNodeList = editorNodeList;
        // Flatten nodes into a map
        _map = FlattenNodeInto(new Dictionary<string, EditorNode>(), EditorNodeList.Root);

        return this;
    }

    public EditorNode? GetNode(string id)
    {
        if (_map.ContainsKey(id))
        {
            return _map[id];
        }
        return null;
    }

    private IDictionary<string, EditorNode> FlattenNodeInto(
        Dictionary<string, EditorNode> dictionary,
        IList<EditorNode> nodeList
    )
    {
        foreach (var node in nodeList)
        {
            dictionary.Add(node.Id, node);
            if (node.Children?.Count > 0)
            {
                FlattenNodeInto(dictionary, node.Children);
            }
        }
        return dictionary;
    }
}
