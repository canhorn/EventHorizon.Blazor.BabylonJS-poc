namespace EventHorizon.Game.Client.Systems.ClientAssets.Api;

public interface ClientAssetState
{
    Option<ClientAsset> Get(string id);
    void Set(ClientAsset clientAsset);
    void Reset();
}
