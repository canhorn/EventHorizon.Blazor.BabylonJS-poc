namespace EventHorizon.Game.Client.Systems.Map.Api
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    public interface IMapState
    {
        [MaybeNull]
        IMapMeshEntity CurrentMap { get; }
        Task DisposeOfMap();
        Task SetMap(
            IMapMeshEntity mapMeshFromHeightMapEntity
        );
    }
}
