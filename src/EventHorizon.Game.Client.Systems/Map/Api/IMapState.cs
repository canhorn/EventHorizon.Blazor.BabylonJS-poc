namespace EventHorizon.Game.Client.Systems.Map.Api
{
    using System.Threading.Tasks;

    public interface IMapState
    {
        IMapMeshEntity CurrentMap { get; }
        Task DisposeOfMap();
        Task SetMap(
            IMapMeshEntity mapMeshFromHeightMapEntity
        );
    }
}
