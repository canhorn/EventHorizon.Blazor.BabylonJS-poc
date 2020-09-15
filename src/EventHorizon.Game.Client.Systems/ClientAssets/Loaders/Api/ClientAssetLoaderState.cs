namespace EventHorizon.Game.Client.Systems.ClientAssets.Loaders.Api
{
    using System;

    public interface ClientAssetLoaderState
    {
        Option<ClientAssetLoader> Get(
            string id
        );

        void Set(
            string id,
            ClientAssetLoader loader
        );
    }
}
