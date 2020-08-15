namespace EventHorizon.Game.Client.Engine.Entity.Vector3Math
{
    using System;
    using System.Numerics;
    using EventHorizon.Game.Client.Engine.Entity.Api;
    using EventHorizon.Game.Client.Engine.Entity.Model;

    public static class Vector3Math
    {
        /**
         * Returns a new Vector3 located for "amount" on the CatmullRom interpolation spline defined by the vectors "value1", "value2", "value3", "value4"
         * @param value1 defines the first control point
         * @param value2 defines the second control point
         * @param value3 defines the third control point
         * @param value4 defines the fourth control point
         * @param amount defines the amount on the spline to use
         * @returns the new Vector3
         */
        public static IVector3 CatmullRom(IVector3 value1, IVector3 value2, IVector3 value3, IVector3 value4, decimal amount)
        {
            var squared = amount * amount;
            var cubed = amount * squared;

            var x = 0.5m * ((((2.0m * value2.X) + ((-value1.X + value3.X) * amount)) +
                (((((2.0m * value1.X) - (5.0m * value2.X)) + (4.0m * value3.X)) - value4.X) * squared)) +
                ((((-value1.X + (3.0m * value2.X)) - (3.0m * value3.X)) + value4.X) * cubed));

            var y = 0.5m * ((((2.0m * value2.Y) + ((-value1.Y + value3.Y) * amount)) +
                (((((2.0m * value1.Y) - (5.0m * value2.Y)) + (4.0m * value3.Y)) - value4.Y) * squared)) +
                ((((-value1.Y + (3.0m * value2.Y)) - (3.0m * value3.Y)) + value4.Y) * cubed));

            var z = 0.5m * ((((2.0m * value2.Z) + ((-value1.Z + value3.Z) * amount)) +
                (((((2.0m * value1.Z) - (5.0m * value2.Z)) + (4.0m * value3.Z)) - value4.Z) * squared)) +
                ((((-value1.Z + (3.0m * value2.Z)) - (3.0m * value3.Z)) + value4.Z) * cubed));

            return new StandardVector3(x, y, z);
        }

        public static double Dot(
            IVector3 facingDirection,
            IVector3 targetDirection
        )
        {
            return Vector3.Dot(
                facingDirection.ToNumerics(),
                targetDirection.ToNumerics()
            );
        }
    }
}
