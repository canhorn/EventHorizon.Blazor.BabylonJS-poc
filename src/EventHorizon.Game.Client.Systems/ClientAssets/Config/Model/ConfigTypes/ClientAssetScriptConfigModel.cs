namespace EventHorizon.Game.Client.Systems.ClientAssets.Config.Model.ConfigTypes
{
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Systems.ClientAssets.Config.Api.ConfigTypes;

    public class ClientAssetScriptConfigModel
        : ClientAssetConfigBase,
        ClientAssetScriptConfig
    {
        public string Script { get; }
        //public int BranchSize { get; }
        //public int TrunkSize { get; }
        //public int Radius { get; }

        public ClientAssetScriptConfigModel(
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
