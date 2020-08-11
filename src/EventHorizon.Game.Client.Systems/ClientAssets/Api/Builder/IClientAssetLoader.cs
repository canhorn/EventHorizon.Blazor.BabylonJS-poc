namespace EventHorizon.Game.Client.Systems.ClientAssets.Api.Builder
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Systems.ClientAssets.Model.Mesh;

    public interface IClientAssetLoader
    {
        string Id { get; }
        Task Load(
            ClientAssetMeshDetails details,
            IClientAsset clientAsset
        );
    }
}
