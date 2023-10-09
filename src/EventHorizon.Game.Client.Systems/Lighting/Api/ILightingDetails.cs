namespace EventHorizon.Game.Client.Systems.Lighting.Api;

using System;
using System.Collections.Generic;
using System.Text;

interface ILightingDetails
{
    List<ILightDetails> Lights { get; }
}
