namespace BabylonJS.Cameras
{
    using BabylonJS.Html;
    using EventHorizon.Blazor.Interop;

    public abstract class Camera : CachedEntity
    {
        protected Camera(
            CachedEntity entity
        )
        {
            ___guid = entity.___guid;
        }

        public void SetRotation(
            Vector3 rotation
        )
        {
            EventHorizonBlazorInteropt.Set(
                this,
                "rotation",
                rotation
            );
        }

        public void SetTarget(
            Vector3 target
        )
        {
            EventHorizonBlazorInteropt.Call(
                this,
                "setTarget",
                target
            );
        }

        public void SetAttachControl(
            Canvas canvas,
            bool preventDefault
        )
        {
            EventHorizonBlazorInteropt.Call(
                this,
                "attachControl",
                canvas,
                preventDefault
            );
        }
    }
}
