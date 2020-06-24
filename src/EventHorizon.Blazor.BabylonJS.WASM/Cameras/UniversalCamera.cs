namespace BabylonJS.Cameras
{
    using System.Threading.Tasks;
    using EventHorizon.Blazor.Interop;

    public class UniversalCamera : Camera
    {
        public static async Task<UniversalCamera> Create(
            string name,
            Vector3 position,
            Scene scene
        ) => new UniversalCamera(
            await EventHorizonBlazorInteropt.New(
                new string[] { "BABYLON", "UniversalCamera" },
                name,
                position,
                scene
            )
        );

        private UniversalCamera(
            CachedEntity entity
        ) : base(
            entity
        ) { }
    }
}
