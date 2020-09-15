namespace EventHorizon.Game.Client.Systems.ClientAssets.Loaders.Model
{
    using System;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;

    public static class ClientAssetsLoaderIdBuilder
    {
        public static string GetId(
            ClientAsset clientAsset
        )
        {
            return GetId(
                clientAsset.Type,
                clientAsset.Config?.Type ?? string.Empty
            );
        }

        public static string GetId(
            string clientAssetType,
            string clientAssetConfigType
        )
        {
            if (string.IsNullOrEmpty(
                clientAssetConfigType
            ))
            {
                return $"{clientAssetType}";
            }
            return $"{clientAssetType}::{clientAssetConfigType}";
        }
    }
}
