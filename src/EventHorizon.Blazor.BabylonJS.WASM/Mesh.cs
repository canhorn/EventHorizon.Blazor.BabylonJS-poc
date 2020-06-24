namespace BabylonJS
{
    using System.Threading.Tasks;
    using EventHorizon.Blazor.Interop;

    public class Mesh : CachedEntity
    {
        public static async Task<Mesh> CreateBox(
            string name,
            double size,
            Scene scene
        )
        {
            var entity = await EventHorizonBlazorInteropt.Func(
                new string[] { "BABYLON", "Mesh", "CreateBox" },
                name,
                size,
                scene
            );
            return new Mesh(
                entity
            );
        }

        private Mesh(
            CachedEntity entity
        )
        {
            ___guid = entity.___guid;
        }
    }
}
