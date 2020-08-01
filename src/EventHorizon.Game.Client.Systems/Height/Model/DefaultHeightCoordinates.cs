using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Systems.Height.Api;

namespace EventHorizon.Game.Client.Systems.Height.Model
{
    internal class DefaultHeightCoordinates
        : IHeightCoordinates
    {
        public decimal getHeightAtCoordinates(
            decimal x,
            decimal z
        )
        {
            return 0;
        }
    }
}
