namespace EventHorizon.Game.Client.Systems.ClientAssets.Config.State
{
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Systems.ClientAssets.Config.Api;
    using EventHorizon.Game.Client.Systems.ClientAssets.Config.Model;

    public class StandardClientAssetConfigBuilderState
        : ClientAssetConfigBuilderState
    {
        private readonly IDictionary<string, ClientAssetConfigTypeBuilder> _buildTypes = new Dictionary<string, ClientAssetConfigTypeBuilder>();

        public ClientAssetConfigTypeBuilder Get(
            string type
        )
        {
            if (_buildTypes.TryGetValue(
                type,
                out var value
            ))
            {
                return value;
            }

            return new StandardClientAssetConfigTypeBuilder(
                (data) => new ClientAssetConfigBase(data)
            );
        }

        public void Set(
            string type,
            ClientAssetConfigTypeBuilder builder
        )
        {
            _buildTypes[type] = builder;
        }
    }
}
