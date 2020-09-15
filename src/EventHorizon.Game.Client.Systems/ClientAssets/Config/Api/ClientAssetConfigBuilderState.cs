namespace EventHorizon.Game.Client.Systems.ClientAssets.Config.Api
{
    using System;

    public interface ClientAssetConfigBuilderState
    {
        ClientAssetConfigTypeBuilder Get(
            string type
        );
        void Set(
            string type,
            ClientAssetConfigTypeBuilder builder
        );
    }
}
