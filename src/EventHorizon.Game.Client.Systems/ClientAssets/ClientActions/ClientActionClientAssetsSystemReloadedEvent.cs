namespace EventHorizon.Game.Client.Systems.ClientAssets.ClientActions;

using System.Collections.Generic;
using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
using EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes;
using EventHorizon.Game.Client.Systems.ClientAssets.Api;
using EventHorizon.Game.Client.Systems.ClientAssets.Model;

[ClientAction("CLIENT_ASSETS_SYSTEM_RELOADED_CLIENT_ACTION_EVENT")]
public class ClientActionClientAssetsSystemReloadedEvent : IClientAction
{
    public IEnumerable<ClientAsset> ClientAssetList { get; }

    public ClientActionClientAssetsSystemReloadedEvent(IClientActionDataResolver resolver)
    {
        ClientAssetList = resolver.Resolve<List<ClientAssetModel>>("clientAssetList");
    }
}
