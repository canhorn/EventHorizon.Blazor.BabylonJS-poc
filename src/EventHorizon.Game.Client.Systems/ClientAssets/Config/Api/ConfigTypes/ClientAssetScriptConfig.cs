namespace EventHorizon.Game.Client.Systems.ClientAssets.Config.Api.ConfigTypes
{
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;

    public interface ClientAssetScriptConfig
        : ClientAssetConfig
    {
        string Script { get; }
        //int BranchSize { get; }
        //int TrunkSize { get; }
        //int Radius { get; }
    }
}
