namespace BabylonJS
{
    using System.Threading.Tasks;
    using EventHorizon.Blazor.Interop;

    public class Vector3 : CachedEntity
    {
        public static async Task<Vector3> Create(
            double x, // TODO: These might need to be decimal
            double y,
            double z
        )
        {
            var entity = await EventHorizonBlazorInteropt.New(
                new string[] { "BABYLON", "Vector3" },
                x,
                y,
                z
            );
            return new Vector3(
                entity
            );
        }

        public Vector3(
            CachedEntity entity
        )
        {
            ___guid = entity.___guid;
        }

        public decimal X()
        {
            return EventHorizonBlazorInteropt.Get<decimal>(
                ___guid,
                "x"
            );
        }

        public decimal Y()
        {
            return EventHorizonBlazorInteropt.Get<decimal>(
                ___guid,
                "y"
            );
        }

        public decimal Z()
        {
            return EventHorizonBlazorInteropt.Get<decimal>(
                ___guid,
                "z"
            );
        }
    }
}
