namespace EventHorizon.Game.Client.Systems.ClientAssets.Api.Configs
{
    public interface IClientAssetScriptConfig
        : IClientAssetConfig
    {
        string Script { get; }
        //int BranchSize { get; }
        //int TrunkSize { get; }
        //int Radius { get; }
    }
}
