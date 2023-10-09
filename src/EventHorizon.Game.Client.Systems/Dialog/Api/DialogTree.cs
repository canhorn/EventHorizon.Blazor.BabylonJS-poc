namespace EventHorizon.Game.Client.Systems.Dialog.Api;

using EventHorizon.Game.Client.Systems.ClientAssets.Api;

public interface DialogTree : ClientAssetConfig
{
    string Id { get; }
    DialogTreeNode Root { get; }
}
