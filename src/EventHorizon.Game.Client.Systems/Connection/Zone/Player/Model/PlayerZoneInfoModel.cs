namespace EventHorizon.Game.Client.Systems.Connection.Zone.Player.Model
{
    using System.Collections.Generic;
    using System.Linq;
    using EventHorizon.Game.Client.Engine.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Model;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;
    using EventHorizon.Game.Client.Systems.ClientAssets.Model;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Api;
    using EventHorizon.Game.Client.Systems.Map.Api;
    using EventHorizon.Game.Client.Systems.Map.Model;

    public class PlayerZoneInfoModel
        : IPlayerZoneInfo
    {
        public PlayerZoneDetailsModel Player { get; set; }
        IPlayerZoneDetails IPlayerZoneInfo.Player => Player;
        public MapMeshDetailsModel MapMesh { get; set; }
        IMapMeshDetails IPlayerZoneInfo.MapMesh => MapMesh;

        public List<ClientAssetModel> ClientAssetList { get; set; }
        IEnumerable<IClientAsset> IPlayerZoneInfo.ClientAssetList => ClientAssetList;
        public List<ObjectEntityDetailsModel> ClientEntityList { get; set; }
        IEnumerable<IObjectEntityDetails> IPlayerZoneInfo.ClientEntityList => ClientEntityList;
    }
}
