namespace EventHorizon.Game.Client.Systems.Player.Modules.Camera.Model;

using System;
using BabylonJS;
using EventHorizon.Game.Client.Engine.Entity.Model;
using EventHorizon.Game.Client.Engine.Rendering.Api;
using EventHorizon.Game.Client.Engine.Systems.Camera.Model;
using EventHorizon.Game.Client.Systems.Player.Api;

public class BabylonJSUniversalCamera : BabylonJSCameraBase<UniversalCamera>
{
    private readonly IPlayerEntity _entity;

    public BabylonJSUniversalCamera(string name, IPlayerEntity entity)
        : base(
            new UniversalCamera(
                name,
                new BabylonJSVector3(0, 10, -30),
                GameServiceProvider.GetService<IRenderingScene>().GetBabylonJSScene().Scene
            )
        )
    {
        _entity = entity;
        _camera.setTarget(Vector3.Zero());
    }
}
