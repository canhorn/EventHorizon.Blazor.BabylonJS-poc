﻿namespace EventHorizon.Game.Client.Engine.Entity.Model
{
    using System.Text.Json.Serialization;
    using EventHorizon.Blazor.Interop;
    using EventHorizon.Game.Client.Engine.Entity.Api;

    [JsonConverter(typeof(CachedEntityConverter))]
    public class BabylonJSVector3
        : BabylonJS.Vector3, IVector3
    {
        public BabylonJSVector3(
            decimal x, 
            decimal y, 
            decimal z
        ) : base(x, y, z)
        {
        }

        public decimal X => x;
        public decimal Y => y;
        public decimal Z => z;
    }
}