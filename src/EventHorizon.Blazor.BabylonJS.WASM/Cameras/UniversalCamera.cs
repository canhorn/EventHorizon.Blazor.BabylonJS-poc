namespace BabylonJS.Cameras
{
    using System.Threading.Tasks;
    using EventHorizon.Blazor.Interop;

    public class UniversalCamera : Camera
    {
        public UniversalCamera(
            string name,
            Vector3 position,
            Scene scene
        ) : base(
            EventHorizonBlazorInteropt.New(
                new string[] { "BABYLON", "UniversalCamera" },
                name,
                position,
                scene
            )
        ) { }
    }
}
