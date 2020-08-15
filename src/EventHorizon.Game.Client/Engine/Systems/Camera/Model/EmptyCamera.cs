namespace EventHorizon.Game.Client.Engine.Systems.Camera.Model
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Systems.Mesh.Api;

    internal class EmptyCamera
        : ICamera
    {
        public long ClientId { get; } = -1;

        public void AttachControl()
        {
            throw new System.NotImplementedException();
        }

        public Task Dispose()
        {
            throw new System.NotImplementedException();
        }

        public Task Initialize()
        {
            throw new System.NotImplementedException();
        }

        public bool IsInFrustum(
            IEngineMesh mesh
        )
        {
            throw new System.NotImplementedException();
        }

        public Task PostInitialize()
        {
            throw new System.NotImplementedException();
        }

        public void SetAsActive()
        {
            throw new System.NotImplementedException();
        }
    }
}