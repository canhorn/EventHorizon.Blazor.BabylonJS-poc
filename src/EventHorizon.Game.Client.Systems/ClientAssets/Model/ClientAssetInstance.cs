namespace EventHorizon.Game.Client.Systems.ClientAssets.Model
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Entity.Api;
    using EventHorizon.Game.Client.Engine.Lifecycle.Model;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Model;
    using EventHorizon.Game.Client.Engine.Systems.Mesh.Api;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;

    public class ClientAssetInstance
        : ClientLifecycleEntityBase,
        IClientAssetInstance
    {
        public string AssetInstanceId { get; }
        public IEngineMesh Mesh { get; }

        public ClientAssetInstance(
            string assetInstanceId,
            IEngineMesh mesh,
            IVector3 position
        ) : base(
            new ObjectEntityDetailsModel
            {
                Id = -1,
                Type = "ClientAssetInstance",
                Name = $"ClientAssetInstance_{assetInstanceId}",
                IsReadOnly = true,
            }
        )
        {
            AssetInstanceId = assetInstanceId;
            Mesh = mesh;
            Mesh.Position.Set(
                position.X,
                position.Y,
                position.Z
            );
            Mesh.MetaData.Add(
                "clientAsset",
                this
            );
        }

        public override Task Initialize()
        {
            Mesh.SetEnabled(true);
            return Task.CompletedTask;
        }

        public override Task PostInitialize()
        {
            return base.PostInitialize();
        }

        public override Task Dispose()
        {
            Mesh.Dispose();
            return base.Dispose();
        }

        public override Task Draw()
        {
            return Task.CompletedTask;
        }

        public override Task Update()
        {
            return base.Update();
        }
    }
}
