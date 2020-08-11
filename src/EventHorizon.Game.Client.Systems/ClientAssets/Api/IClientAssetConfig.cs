namespace EventHorizon.Game.Client.Systems.ClientAssets.Api
{
    public interface IClientAssetConfig
    {
        string Type { get; }

        int GetInt(
            string key
        );
        float GetFloat(
            string key
        );
        string GetString(
            string key
        );
    }
}
