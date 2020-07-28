namespace EventHorizon.Game.Client.Systems.Connection.Zone.Player.Model
{
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
    }
}
