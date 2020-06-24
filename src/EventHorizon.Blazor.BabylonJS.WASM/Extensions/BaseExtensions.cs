using System.Numerics;
using EventHorizon.Blazor.Interop;

namespace BabylonJS
{
    public static class CachedEntityExtensions
    {
        public static string Name(
            this CachedEntity entity
        )
        {
            return EventHorizonBlazorInteropt.Get<string>(
                entity.___guid,
                "name"
            );
        }

        public static Vector3 Position(
            this CachedEntity entity
        )
        {
            return new Vector3(
                EventHorizonBlazorInteropt.Get<CachedEntity>(
                    entity.___guid,
                    "position"
                )
            );
        }
    }
}
