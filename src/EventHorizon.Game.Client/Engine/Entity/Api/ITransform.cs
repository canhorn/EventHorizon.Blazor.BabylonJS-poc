using System;
using System.Collections.Generic;
using System.Text;

namespace EventHorizon.Game.Client.Engine.Entity.Api
{
    public interface ITransform
    {
        IVector3 Position { get; }
        IVector3 Rotation { get; }
        IVector3 Scaling { get; }
        long? ScalingDeterminant { get; }
    }
}
