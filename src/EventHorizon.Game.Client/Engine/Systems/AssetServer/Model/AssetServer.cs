using System;
using System.Collections.Generic;
using System.Text;
using EventHorizon.Game.Client.Core.Configuration;

namespace EventHorizon.Game.Client.Engine.Systems.AssetServer.Model
{
    public static class AssetServer
    {
        public static string CreateAssetLocationUrl(
            string path
        )
        {
            var assetServerUrl = Configuration.GetConfig<string>("ASSET_SERVER");
            return $"{assetServerUrl}{path}";
        }
    }
}
