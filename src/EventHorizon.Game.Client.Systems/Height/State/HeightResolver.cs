namespace EventHorizon.Game.Client.Systems.Height.State;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Systems.Height.Api;
using EventHorizon.Game.Client.Systems.Height.Model;

public class HeightResolver : IHeightResolver, ISetHeightResolver
{
    private IHeightCoordinates _coordinates = new DefaultHeightCoordinates();

    public decimal FindHeight(decimal x, decimal z) =>
        _coordinates.getHeightAtCoordinates(x, z);

    public void setCoordinates(IHeightCoordinates coordinates)
    {
        if (coordinates == null)
        {
            return;
        }
        _coordinates = coordinates;
    }
}
