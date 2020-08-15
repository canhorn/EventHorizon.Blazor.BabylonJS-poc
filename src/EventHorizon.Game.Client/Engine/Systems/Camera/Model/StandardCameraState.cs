namespace EventHorizon.Game.Client.Engine.Systems.Camera.Model
{
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Core.Exceptions;
    using EventHorizon.Game.Client.Engine.Systems.Camera.Api;

    public class StandardCameraState
        : ICameraState
    {
        private readonly IDictionary<string, ICamera> _cameraMap = new Dictionary<string, ICamera>();

        public ICamera ActiveCamera { get; private set; } = new EmptyCamera();

        public void Manage(
            string name,
            ICamera camera
        )
        {
            if (_cameraMap.TryGetValue(
                name,
                out var existingCamera
            ))
            {
                existingCamera.Dispose();
            }
            _cameraMap[name] = camera;
            camera.Initialize();
        }

        public void SetActive(
            string name
        )
        {
            if (_cameraMap.TryGetValue(
                name,
                out var camera
            ))
            {
                ActiveCamera = camera;
                camera.SetAsActive();
                return;
            }
#if DEBUG
            throw new GameException(
                "camera_not_found",
                "Camera not found"
            );
#endif
        }

        public void Dispose(
            string name
        )
        {
            if (_cameraMap.TryGetValue(
                name,
                out var camera
            ))
            {
                camera.Dispose();
                _cameraMap.Remove(
                    name
                );
            }
        }
    }
}
