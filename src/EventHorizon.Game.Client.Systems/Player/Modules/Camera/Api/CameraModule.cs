namespace EventHorizon.Game.Client.Systems.Player.Modules.Camera.Api
{
    using System;
    using EventHorizon.Game.Client.Engine.Systems.Module.Api;

    public interface CameraModule
        : IModule
    {
        public static string MODULE_NAME { get; } = "CAMERA_MODULE_NAME";
    }
}
