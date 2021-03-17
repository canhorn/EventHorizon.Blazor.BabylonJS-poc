namespace EventHorizon.Game.Client.Systems.Dialog.Model
{
    using System;
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Systems.ClientAssets.Config.Model;
    using EventHorizon.Game.Client.Systems.Dialog.Api;

    public class DialogTreeModel
        : ClientAssetConfigBase,
        DialogTree
    {
        public static string CLIENT_ASSET_TYPE => "DIALOG";

        public string Id { get; }
        public DialogTreeNode Root { get; }

        public DialogTreeModel(
            IDictionary<string, object> data
        ) : base(CLIENT_ASSET_TYPE, data)
        {
            Id = GetString("id");
            Root = data["root"].To<DialogTreeNodeModel>();
        }
    }
}
