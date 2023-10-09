namespace EventHorizon.Game.Client.Engine.Entity.Model;

using System.Numerics;

using EventHorizon.Game.Client.Engine.Entity.Api;

public class Vector3Model : StandardVector3, IVector3
{
    public new decimal X
    {
        get { return (decimal)_vector.X; }
        set { _vector.X = (float)value; }
    }
    public new decimal Y
    {
        get { return (decimal)_vector.Y; }
        set { _vector.Y = (float)value; }
    }
    public new decimal Z
    {
        get { return (decimal)_vector.Z; }
        set { _vector.Z = (float)value; }
    }

    public Vector3Model()
        : base(Vector3.Zero) { }

    public Vector3Model(IVector3 vector3)
        : base(vector3) { }

    public Vector3Model(Vector3 vector3)
        : base(vector3) { }

    public Vector3Model(decimal x, decimal y, decimal z)
        : base(x, y, z) { }

    public override string ToString()
    {
        return $"Vector3Model({X}, {Y}, {Z})";
    }
}
