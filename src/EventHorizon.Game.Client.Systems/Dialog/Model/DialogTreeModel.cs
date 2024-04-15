namespace EventHorizon.Game.Client.Systems.Dialog.Model;

using System.Collections.Generic;
using EventHorizon.Game.Client.Systems.ClientAssets.Config.Model;
using EventHorizon.Game.Client.Systems.Dialog.Api;

public class DialogTreeModel : ClientAssetConfigBase, DialogTree
{
    public static string CLIENT_ASSET_TYPE { get; } = "DIALOG";

    public string Id { get; }
    public DialogTreeNode Root { get; }

    public DialogTreeModel(IDictionary<string, object> data)
        : base(data)
    {
        Id = GetString("id");
        Root = data["root"].To(() => new DialogTreeNodeModel());
    }
}
