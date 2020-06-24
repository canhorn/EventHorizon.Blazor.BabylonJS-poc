namespace BabylonJS
{
    using System.Threading.Tasks;
    using EventHorizon.Blazor.Interop;

    public class PointLight : CachedEntity
    {
        public static async Task<PointLight> Create(
            string name,
            Vector3 position,
            Scene scene
        )
        {
            var entity = await EventHorizonBlazorInteropt.New(
                new string[] { "BABYLON", "PointLight" },
                name,
                position,
                scene
            );
            return new PointLight(
                entity
            );
        }

        private PointLight(
            CachedEntity entity
        )
        {
            ___guid = entity.___guid;
        }
    }
}
