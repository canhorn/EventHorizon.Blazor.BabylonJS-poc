namespace BabylonJS.Cameras
{
    using System.Threading.Tasks;
    using EventHorizon.Blazor.Interop;

    public class FreeCamera : Camera
    {
        public FreeCamera(
            string name,
            Vector3 position,
            Scene scene
        ) : base(
            EventHorizonBlazorInteropt.New(
                new string[] { "BABYLON", "FreeCamera" },
                name,
                position,
                scene
            )
        ) { }
    }
}
