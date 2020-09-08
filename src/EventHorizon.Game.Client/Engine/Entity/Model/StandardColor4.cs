namespace EventHorizon.Game.Client.Engine.Entity.Model
{
    using System;
    using EventHorizon.Game.Client.Engine.Entity.Api;

    public class StandardColor4
        : IColor4
    {
        public decimal R { get; set; }
        public decimal G { get; set; }
        public decimal B { get; set; }
        public decimal A { get; set; }

        public StandardColor4(
            IColor4 color4
        )
        {
            R = color4.R;
            G = color4.G;
            B = color4.B;
            A = color4.A;
        }

        public StandardColor4(
            decimal r,
            decimal g,
            decimal b,
            decimal a
        )
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
    }
}
