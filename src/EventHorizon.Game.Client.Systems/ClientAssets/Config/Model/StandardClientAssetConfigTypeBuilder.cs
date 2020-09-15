namespace EventHorizon.Game.Client.Systems.ClientAssets.Config.Model
{
    using System;
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;
    using EventHorizon.Game.Client.Systems.ClientAssets.Config.Api;

    public class StandardClientAssetConfigTypeBuilder
        : ClientAssetConfigTypeBuilder
    {
        private readonly Func<IDictionary<string, object>, ClientAssetConfig> _build;

        public string Type { get; }

        public StandardClientAssetConfigTypeBuilder(
            string type,
            Func<IDictionary<string, object>, ClientAssetConfig> build
        )
        {
            Type = type;
            _build = build;
        }

        public ClientAssetConfig Build(
            IDictionary<string, object> data
        )
        {
            return _build(
                data
            );
        }
    }
}
