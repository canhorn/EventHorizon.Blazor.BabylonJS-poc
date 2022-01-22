namespace EventHorizon.Game.Client.Systems.ClientAssets.State;

using System.Collections.Generic;

using EventHorizon.Game.Client.Systems.ClientAssets.Api;

internal class StandardClientAssetInstanceState : ClientAssetInstanceState
{
    private readonly IDictionary<string, ClientAssetInstance> _map =
        new Dictionary<string, ClientAssetInstance>();

    public Option<ClientAssetInstance> Get(string id)
    {
        if (_map.TryGetValue(id, out var clientAssetInstance))
        {
            return clientAssetInstance.ToOption();
        }
        return new Option<ClientAssetInstance>(null);
    }

    public void Set(ClientAssetInstance clientAsset)
    {
        _map[clientAsset.AssetInstanceId] = clientAsset;
    }

    public void Remove(string id)
    {
        _map.Remove(id);
    }

    public void Clear()
    {
        _map.Clear();
    }
}
