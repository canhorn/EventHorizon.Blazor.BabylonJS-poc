namespace EventHorizon.Game.Client.Systems.ClientAssets.Builder
{
    using System;
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Core.Builder.Api;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;
    using EventHorizon.Game.Client.Systems.ClientAssets.Config.Api;

    public class StandardClientAssetConfigBuilder
        : IBuilder<ClientAssetConfig, IDictionary<string, object>>
    {
        private readonly ClientAssetConfigBuilderState _builderState;

        public StandardClientAssetConfigBuilder(
            ClientAssetConfigBuilderState builderState
        )
        {
            _builderState = builderState;
        }

        public ClientAssetConfig Build(
            IDictionary<string, object> data
        )
        {
            return _builderState.Get(
                GetString(data, "type")
            ).Build(data);
        }

        public static string GetString(
            IDictionary<string, object> data,
            string key
        )
        {
            if (data.ContainsKey(key))
            {
                return data[key].To<string>() ?? string.Empty;
            }
            return string.Empty;
        }
    }
}
