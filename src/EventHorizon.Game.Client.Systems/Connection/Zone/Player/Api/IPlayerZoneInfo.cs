namespace EventHorizon.Game.Client.Systems.Connection.Zone.Player.Api
{
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;
    using EventHorizon.Game.Client.Systems.Map.Api;

    public interface IPlayerZoneInfo
    {
        IPlayerZoneDetails Player { get; }
        IMapMeshDetails MapMesh { get; }
        IEnumerable<IClientAsset> ClientAssetList { get; }
        IEnumerable<IObjectEntityDetails> ClientEntityList { get; }
    }
}