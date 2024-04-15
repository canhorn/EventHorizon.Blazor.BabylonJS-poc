namespace EventHorizon.Game.Client.Systems.Map.Model;

using BabylonJS;
using EventHorizon.Game.Client.Systems.Height.Api;

public class BabylonJSHeightCoordinates : IHeightCoordinates
{
    private readonly GroundMesh _mesh;

    public BabylonJSHeightCoordinates(GroundMesh mesh)
    {
        _mesh = mesh;
    }

    public decimal getHeightAtCoordinates(decimal x, decimal z) =>
        _mesh.getHeightAtCoordinates(x, z);
}
