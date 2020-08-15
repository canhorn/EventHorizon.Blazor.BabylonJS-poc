namespace EventHorizon.Game.Client.Engine.Entity.Api
{
    using BabylonJS;
    using EventHorizon.Game.Client.Engine.Entity.Model;

    public static class BabylonJSVector3Extensions
    {
        public static Vector3 ToBabylonJS(
            this IVector3 vector3
        )
        {
            if (vector3 is Vector3 typedVector3)
            {
                return typedVector3;
            }
            return new BabylonJSVector3(
                vector3
            );
        }

        public static IVector3 ToStandardVector3(
            this Vector3 vector3
        ) => new StandardVector3(
            vector3.x,
            vector3.y,
            vector3.z
        );
    }
}
