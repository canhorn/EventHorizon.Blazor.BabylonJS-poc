using System;
using System.Collections.Generic;
using System.Text;

namespace EventHorizon.Game.Client.Systems.Lighting.Api
{
    interface ILightingDetails
    {
        List<ILightDetails> Lights { get; }
    }
}
