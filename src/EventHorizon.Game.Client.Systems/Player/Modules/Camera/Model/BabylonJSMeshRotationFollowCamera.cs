﻿namespace EventHorizon.Game.Client.Systems.Player.Modules.Camera.Model;

using BabylonJS;
using EventHorizon.Game.Client.Engine.Rendering.Api;
using EventHorizon.Game.Client.Engine.Systems.Camera.Model;
using EventHorizon.Game.Client.Engine.Systems.Mesh.Model;
using EventHorizon.Game.Client.Systems.Local.Modules.MeshManagement.Api;
using EventHorizon.Game.Client.Systems.Player.Api;

public class BabylonJSMeshRotationFollowCamera : BabylonJSCameraBase<ArcFollowCamera>
{
    private static readonly decimal DEFAULT_X_ROTATION = -(decimal)System.Math.PI / 2;
    private static readonly decimal DEFAULT_Y_ROTATION = (decimal)System.Math.PI / 4;
    private static readonly decimal DEFAULT_RADIUS = 15;

    public BabylonJSMeshRotationFollowCamera(string name, IPlayerEntity entity)
        : base(
            new ArcFollowCamera(
                name,
                DEFAULT_X_ROTATION,
                DEFAULT_Y_ROTATION,
                DEFAULT_RADIUS,
                entity
                    .GetModule<IMeshModule>(IMeshModule.MODULE_NAME)
                    ?.Mesh?.To<BabylonJSEngineMesh>()
                    ?.Mesh!,
                GameServiceProvider.GetService<IRenderingScene>().GetBabylonJSScene().Scene
            )
        )
    {
        _camera.speed = -1;
    }
}
