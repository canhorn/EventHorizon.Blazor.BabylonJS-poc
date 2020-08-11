namespace EventHorizon.Game.Client.Engine.Rendering.Model.Camera
{
    using System;
    using BabylonJS;
    using EventHorizon.Game.Client.Engine.Rendering.Api.Camera;
    using EventHorizon.Game.Client.Engine.Systems.Mesh.Api;
    using EventHorizon.Game.Client.Engine.Systems.Mesh.Model;

    public class BabylonJSCamera
        : ICamera
    {
        private readonly Camera _activeCamera;

        public BabylonJSCamera(
            Camera activeCamera
        )
        {
            _activeCamera = activeCamera;
        }

        public bool IsInFrustum(IEngineMesh engineMesh)
        {
            if (engineMesh is BabylonJSEngineMesh mesh)
            {
                return _activeCamera.isInFrustum(
                    mesh.Mesh
                );
            }
            return false;
        }
    }
}
