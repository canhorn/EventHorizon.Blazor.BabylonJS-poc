namespace EventHorizon.Game.Client.Engine.Rendering.Api.Camera
{
    using System;
    using EventHorizon.Game.Client.Engine.Systems.Mesh.Api;

    public interface ICamera
    {
        bool IsInFrustum(IEngineMesh mesh);
    }
}
