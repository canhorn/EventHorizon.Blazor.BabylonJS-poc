namespace EventHorizon.Game.Client.Engine.Systems.Entity.Model;

using EventHorizon.Game.Client.Engine.Entity.Api;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;

public class ServerVector3 : IServerVector3
{
    public static ServerVector3 Zero() =>
        new()
        {
            X = 0,
            Y = 0,
            Z = 0,
        };

    public static ServerVector3 One() =>
        new()
        {
            X = 1,
            Y = 1,
            Z = 1,
        };

    public decimal X { get; set; }
    public decimal Y { get; set; }
    public decimal Z { get; set; }

    public ServerVector3() { }

    public ServerVector3(IVector3 vector3)
    {
        X = vector3.X;
        Y = vector3.Y;
        Z = vector3.Z;
    }
}
