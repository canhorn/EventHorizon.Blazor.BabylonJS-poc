namespace EventHorizon.Game.Client.Engine.Entity.Api;

using System.Numerics;

public static class StandardVector3Extensions
{
    public static Vector3 ToNumerics(this IVector3 vector3)
    {
        if (vector3.GetType() == typeof(Vector3))
        {
            return vector3.To<Vector3>();
        }
        return new Vector3(
            (float)vector3.X,
            (float)vector3.Y,
            (float)vector3.Z
        );
    }
}
