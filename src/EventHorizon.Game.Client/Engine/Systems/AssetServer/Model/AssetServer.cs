namespace EventHorizon.Game.Client.Engine.Systems.AssetServer.Model
{
    using EventHorizon.Game.Client.Core.Configuration;

    public static class AssetServer
    {
        public static string CreateAssetLocationUrl(
            string path
        )
        {
            var assetServerUrl = Configuration.GetConfig<string>(
                "ASSET_SERVER"
            );
            return $"{assetServerUrl}{path}";
        }
    }
}
