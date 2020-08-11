namespace EventHorizon.Game.Client.Systems.ClientAssets.Api
{
    using EventHorizon.Game.Client.Engine.Systems.Mesh.Api;

    public interface IClientAssetMeshCache
    {
        bool Cached(
            string id
        );
        IEngineMesh Get(
            string id
        );
        void Set(
            string id,
            IEngineMesh mesh
        );
    }
}
