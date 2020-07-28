using System;
using System.Collections.Generic;
using System.Text;
using EventHorizon.Game.Client.Engine.Entity.Api;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;

namespace EventHorizon.Game.Client.Engine.Entity.Model
{
    public class TransformModel
        : ITransform
    {
        public IVector3 Position { get; }
        public IVector3 Rotation { get; }
        public IVector3 Scaling { get; }
        public long? ScalingDeterminant { get; }

        public TransformModel(
            IVector3 position, 
            IVector3 rotation, 
            IVector3 scaling, 
            long? scalingDeterminant
        )
        {
            Position = position;
            Rotation = rotation;
            Scaling = scaling;
            ScalingDeterminant = scalingDeterminant;
        }
    }
}
