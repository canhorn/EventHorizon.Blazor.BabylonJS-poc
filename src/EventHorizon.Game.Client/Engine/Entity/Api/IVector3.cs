using System;
using System.Collections.Generic;
using System.Text;

namespace EventHorizon.Game.Client.Engine.Entity.Api
{
    public interface IVector3
    {
        decimal X { get; }
        decimal Y { get; }
        decimal Z { get; }
    }
}
