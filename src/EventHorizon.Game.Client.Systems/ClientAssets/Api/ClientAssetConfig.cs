namespace EventHorizon.Game.Client.Systems.ClientAssets.Api
{
    public interface ClientAssetConfig
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
