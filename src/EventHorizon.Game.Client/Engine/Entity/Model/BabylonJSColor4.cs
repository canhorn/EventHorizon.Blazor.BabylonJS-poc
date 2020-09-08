namespace EventHorizon.Game.Client.Engine.Entity.Model
{
    using System;
    using System.Text.Json.Serialization;
    using EventHorizon.Blazor.Interop;
    using EventHorizon.Game.Client.Engine.Entity.Api;

    [JsonConverter(typeof(CachedEntityConverter<BabylonJSColor4>))]
    public class BabylonJSColor4
        : BabylonJS.Color4,
        IColor4
    {
        public decimal R => r;
        public decimal G => g;
        public decimal B => b;
        public decimal A => a;

        public BabylonJSColor4(
            decimal r,
            decimal g,
            decimal b,
            decimal a
        ) : base(r, g, b, a)
        {
        }

        public BabylonJSColor4(
            IColor4 color
        ) : base(color.R, color.G, color.B, color.A)
        {
        }

        public BabylonJSColor4(
            BabylonJS.Color4 color4
        ) : base(color4)
        {
        }
    }
}
