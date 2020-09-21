namespace EventHorizon.Game.Client.Systems.ClientAssets.Api.Mesh
{
    using EventHorizon.Game.Client.Engine.Systems.Mesh.Api;

    public interface ClientAssetMeshCache
    {
        bool Cached(
            string id
        );
        Option<IEngineMesh> Get(
            string id
        );
        void Set(
            string id,
            IEngineMesh mesh
        );
        void Clear();
    }
}
