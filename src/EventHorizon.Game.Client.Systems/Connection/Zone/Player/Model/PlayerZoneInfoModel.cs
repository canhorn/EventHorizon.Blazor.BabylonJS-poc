namespace EventHorizon.Game.Client.Systems.Connection.Zone.Player.Model
{
    using System.Collections.Generic;
    using System.Linq;
    using EventHorizon.Game.Client.Engine.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Model;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;
    using EventHorizon.Game.Client.Systems.ClientAssets.Model;
    using EventHorizon.Game.Client.Systems.ClientScripts.Api;
    using EventHorizon.Game.Client.Systems.ClientScripts.Model;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Api;
    using EventHorizon.Game.Client.Systems.Map.Api;
    using EventHorizon.Game.Client.Systems.Map.Model;

    public class PlayerZoneInfoModel
        : IPlayerZoneInfo
    {
        public PlayerZoneDetailsModel Player { get; set; } = new PlayerZoneDetailsModel();
        IPlayerZoneDetails IPlayerZoneInfo.Player => Player;
        public MapMeshDetailsModel MapMesh { get; set; } = new MapMeshDetailsModel();
        IMapMeshDetails IPlayerZoneInfo.MapMesh => MapMesh;

        public List<ClientAssetModel> ClientAssetList { get; set; } = new List<ClientAssetModel>();
        IEnumerable<IClientAsset> IPlayerZoneInfo.ClientAssetList => ClientAssetList;
        public List<ObjectEntityDetailsModel> ClientEntityList { get; set; } = new List<ObjectEntityDetailsModel>();
        IEnumerable<IObjectEntityDetails> IPlayerZoneInfo.ClientEntityList => ClientEntityList;

        public List<ObjectEntityDetailsModel> EntityList { get; set; } = new List<ObjectEntityDetailsModel>();
        IEnumerable<IObjectEntityDetails> IPlayerZoneInfo.EntityList => EntityList;

        public ClientScriptsAssemblyDetails ClientScriptsAssemblyDetails { get; set; } = new ClientScriptsAssemblyDetails();
        IClientScriptsAssemblyDetails IPlayerZoneInfo.ClientScriptsAssemblyDetails => ClientScriptsAssemblyDetails;
    }
}
