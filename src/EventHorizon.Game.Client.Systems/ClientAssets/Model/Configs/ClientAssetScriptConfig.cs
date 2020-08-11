namespace EventHorizon.Game.Client.Systems.ClientAssets.Model.Configs
{
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api.Configs;

    public class ClientAssetScriptConfig
        : ClientAssetDynamicConfig,
        IClientAssetScriptConfig
    {
        public string Script { get; }
        //public int BranchSize { get; }
        //public int TrunkSize { get; }
        //public int Radius { get; }

        public ClientAssetScriptConfig(
            IDictionary<string, object> data
        ) : base("JavaScript", data)
        {
            Script = GetString("script");
            //BranchSize = GetInt("branchSize");
            //TrunkSize = GetInt("trunkSize");
            //Radius = GetInt("radius");
        }
    }
}
