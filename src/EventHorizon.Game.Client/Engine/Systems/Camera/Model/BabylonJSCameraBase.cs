namespace EventHorizon.Game.Client.Engine.Systems.Camera.Model
{
    using System;
    using System.Threading.Tasks;
    using BabylonJS;
    using EventHorizon.Game.Client.Engine.Canvas.Api;
    using EventHorizon.Game.Client.Engine.Core.Api;
    using EventHorizon.Game.Client.Engine.Entity.Model;
    using EventHorizon.Game.Client.Engine.Lifecycle.Model;
    using EventHorizon.Game.Client.Engine.Rendering.Api;
    using EventHorizon.Game.Client.Engine.Systems.Mesh.Api;
    using EventHorizon.Game.Client.Engine.Systems.Mesh.Model;
    using EventHorizon.Html.Interop;

    public class BabylonJSCameraBase<T>
        : ClientEntityBase,
        ICamera where T : Camera
    {
        protected readonly T _camera;

        public BabylonJSCameraBase(
            T camera
        ) : base(GameServiceProvider.GetService<IIndexPool>().NextIndex())
        {
            _camera = camera;
        }

        public void AttachControl()
        {
            _camera.attachControl(
                GameServiceProvider.GetService<ICanvas>().GetDrawingCanvas<Canvas>(),
                true
            );
        }

        public virtual Task Initialize()
        {
            return Task.CompletedTask;
        }

        public virtual Task PostInitialize()
        {
            return Task.CompletedTask;
        }

        public virtual Task Dispose()
        {
            _camera.dispose();
            return Task.CompletedTask;
        }

        public bool IsInFrustum(
            IEngineMesh engineMesh
        )
        {
            if (engineMesh is BabylonJSEngineMesh mesh)
            {
                return _camera.isInFrustum(
                    mesh.Mesh
                );
            }
            return false;
        }

        public void SetAsActive()
        {
            GameServiceProvider.GetService<IRenderingScene>()
                .GetBabylonJSScene()
                .Scene
                .setActiveCameraByID(
                    _camera.id
                );
        }
    }
}
