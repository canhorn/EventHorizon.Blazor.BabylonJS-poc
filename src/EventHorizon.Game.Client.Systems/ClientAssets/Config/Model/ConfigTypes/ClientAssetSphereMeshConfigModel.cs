namespace EventHorizon.Game.Client.Systems.ClientAssets.Config.Model.ConfigTypes;

using System.Collections.Generic;
using EventHorizon.Game.Client.Systems.ClientAssets.Config.Api.ConfigTypes;

public class ClientAssetSphereMeshConfigModel : ClientAssetConfigBase, ClientAssetSphereMeshConfig
{
    public float Segments { get; }
    public float Diameter { get; }

    public ClientAssetSphereMeshConfigModel(IDictionary<string, object> data)
        : base(data)
    {
        Segments = GetFloat("segments");
        Diameter = GetFloat("diameter");
    }
}
