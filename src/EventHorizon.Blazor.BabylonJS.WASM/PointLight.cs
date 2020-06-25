namespace BabylonJS
{
    using System.Threading.Tasks;
    using EventHorizon.Blazor.Interop;

    public class PointLight : CachedEntity
    {
        public PointLight(
            string name,
            Vector3 position,
            Scene scene
        )
        {
            var entity = EventHorizonBlazorInteropt.New(
                new string[] { "BABYLON", "PointLight" },
                name,
                position,
                scene
            );
            ___guid = entity.___guid;
        }
    }
}
