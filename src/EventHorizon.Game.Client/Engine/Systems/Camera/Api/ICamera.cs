namespace EventHorizon.Game.Client.Engine.Systems.Camera.Model
{
    using System;
    using EventHorizon.Game.Client.Engine.Entity.Api;
    using EventHorizon.Game.Client.Engine.Lifecycle.Api;
    using EventHorizon.Game.Client.Engine.Systems.Mesh.Api;

    public interface ICamera
        : IClientEntity, 
        IInitializableEntity,
        IDisposableEntity
    {
        bool IsInFrustum(
            IEngineMesh mesh
        );
        void AttachControl();
        void SetAsActive();
    }
}
