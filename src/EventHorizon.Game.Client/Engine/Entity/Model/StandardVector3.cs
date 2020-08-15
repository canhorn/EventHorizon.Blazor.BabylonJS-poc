namespace EventHorizon.Game.Client.Engine.Entity.Model
{
    using System;
    using System.Numerics;
    using EventHorizon.Game.Client.Engine.Entity.Api;

    public class StandardVector3
        : IVector3
    {
        public readonly static IVector3 FORWARD_DIRECTION = new StandardVector3(Vector3.UnitZ);
        public readonly static IVector3 RIGHT_DIRECTION = new StandardVector3(Vector3.UnitX);

        protected Vector3 _vector;
        public decimal X => (decimal)_vector.X;
        public decimal Y => (decimal)_vector.Y;
        public decimal Z => (decimal)_vector.Z;

        public StandardVector3(
            IVector3 vector3
        )
        {
            _vector = new Vector3(
                (float)vector3.X,
                (float)vector3.Y,
                (float)vector3.Z
            );
        }

        public StandardVector3(
            Vector3 vector3
        )
        {
            _vector = vector3;
        }

        public StandardVector3(
            decimal x,
            decimal y,
            decimal z
        )
        {
            _vector = new Vector3(
                (float)x,
                (float)y,
                (float)z
            );
        }

        public void Set(
            decimal x,
            decimal y,
            decimal z
        )
        {
            _vector = new Vector3(
                (float)x,
                (float)y,
                (float)z
            );
        }

        public void Set(
            IVector3 vector3
        )
        {
            _vector = new Vector3(
                (float)vector3.X,
                (float)vector3.Y,
                (float)vector3.Z
            );
        }

        public IVector3 Clone()
        {
            return new StandardVector3(
                X,
                Y,
                Z
            );
        }

        public IVector3 Subtract(
            IVector3 currentPosition
        )
        {
            var value = Vector3.Subtract(
                _vector,
                new Vector3(
                    (float)currentPosition.X,
                    (float)currentPosition.Y,
                    (float)currentPosition.Z
                )
            );
            return new StandardVector3(
                (decimal)value.X,
                (decimal)value.Y,
                (decimal)value.Z
            );
        }

        public decimal Length()
        {
            return (decimal)_vector.Length();
        }

        public IVector3 Normalize()
        {
            return new StandardVector3(
                Vector3.Normalize(
                    _vector
                )
            );
        }

        public IVector3 Multiply(
            IVector3 vector3
        )
        {
            return new StandardVector3(
                Vector3.Multiply(
                    _vector,
                    new Vector3(
                        (float)vector3.X,
                        (float)vector3.Y,
                        (float)vector3.Z
                    )
                )
            );
        }

        public void AddInPlace(
            IVector3 vector3
        )
        {
            _vector = Vector3.Add(
                _vector,
                new Vector3(
                    (float)vector3.X,
                    (float)vector3.Y,
                    (float)vector3.Z
                )
            );
        }

        public double LengthSquared()
        {
            return _vector.LengthSquared();
        }
    }
}
