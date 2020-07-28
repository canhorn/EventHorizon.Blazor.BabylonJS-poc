namespace EventHorizon.Game.Client.Systems.Connection.Zone.Player.Api
{
    using EventHorizon.Game.Client.Systems.Map.Api;

    public interface IPlayerZoneInfo
    {
        IPlayerZoneDetails Player { get; }
        IMapMeshDetails MapMesh { get; }
    }
}