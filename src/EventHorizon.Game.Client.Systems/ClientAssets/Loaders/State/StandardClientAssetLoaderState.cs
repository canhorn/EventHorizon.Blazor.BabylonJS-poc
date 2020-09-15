namespace EventHorizon.Game.Client.Systems.ClientAssets.Loaders.State
{
    using System;
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Systems.ClientAssets.Loaders.Api;

    public class StandardClientAssetLoaderState
        : ClientAssetLoaderState
    {
        private readonly IDictionary<string, ClientAssetLoader> _loaders = new Dictionary<string, ClientAssetLoader>();

        public Option<ClientAssetLoader> Get(
            string id
        )
        {
            if (_loaders.TryGetValue(
                id,
                out var value
            ))
            {
                return value.ToOption();
            }
            return new Option<ClientAssetLoader>(
                null
            );
        }

        public void Set(
            string id, 
            ClientAssetLoader loader
        )
        {
            _loaders[id] = loader;
        }
    }
}
