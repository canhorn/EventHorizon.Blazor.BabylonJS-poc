namespace BabylonJS.Cameras
{
    using System.Threading.Tasks;
    using EventHorizon.Blazor.Interop;

    public class FreeCamera : Camera
    {
        public static async Task<FreeCamera> Create(
            string name,
            Vector3 position,
            Scene scene
        ) => new FreeCamera(
            await EventHorizonBlazorInteropt.New(
                new string[] { "BABYLON", "FreeCamera" },
                name,
                position,
                scene
            )
        );

        private FreeCamera(
            CachedEntity entity
        ) : base(
            entity
        ) { }
    }
}
