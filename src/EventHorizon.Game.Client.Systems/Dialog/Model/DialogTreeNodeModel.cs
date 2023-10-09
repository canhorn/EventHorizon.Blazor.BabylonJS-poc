namespace EventHorizon.Game.Client.Systems.Dialog.Model;

using System.Collections.Generic;

using EventHorizon.Game.Client.Systems.Dialog.Api;

public class DialogTreeNodeModel : DialogTreeNode
{
    public string TitleKey { get; set; } = string.Empty;
    public Dictionary<string, object> TitleData { get; set; } =
        new Dictionary<string, object>();
    IDictionary<string, object> DialogTreeNode.TitleData => TitleData;
    public string TextKey { get; set; } = string.Empty;
    public Dictionary<string, object> TextData { get; set; } =
        new Dictionary<string, object>();
    IDictionary<string, object> DialogTreeNode.TextData => TextData;
    public List<DialogTreeActionNodeModel> Actions { get; set; } =
        new List<DialogTreeActionNodeModel>();
    IEnumerable<DialogTreeActionNode> DialogTreeNode.Actions => Actions;
}
