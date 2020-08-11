namespace EventHorizon.Game.Client.Engine.Entity.Model
{
    using System.Text.Json.Serialization;
    using EventHorizon.Blazor.Interop;
    using EventHorizon.Game.Client.Engine.Entity.Api;

    [JsonConverter(typeof(CachedEntityConverter<BabylonJSVector3>))]
    public class BabylonJSVector3
        : BabylonJS.Vector3,
        IVector3
    {
        public BabylonJSVector3(
            decimal x,
            decimal y,
            decimal z
        ) : base(x, y, z)
        {
        }

        public BabylonJSVector3(
            IVector3 vector
        ) : base(vector.X, vector.Y, vector.Z)
        {
        }

        public BabylonJSVector3(
            BabylonJS.Vector3 vector3
        ) : base(vector3)
        {
        }

        public decimal X => x;
        public decimal Y => y;
        public decimal Z => z;

        public void Set(
            decimal x,
            decimal y,
            decimal z
        )
        {
            set(x, y, z);
        }
        public void Set(
            IVector3 vector3
        )
        {
            set(
                vector3.X,
                vector3.Y,
                vector3.Z
            );
        }

        public IVector3 Clone()
        {
            return new BabylonJSVector3(
                clone()
            );
        }

        public IVector3 Subtract(
            IVector3 currentPosition
        )
        {
            return new BabylonJSVector3(
                subtract(
                    currentPosition.ToBabylonJS()
                )
            );
        }

        public decimal Length()
        {
            return length();
        }

        public IVector3 Normalize()
        {
            normalize();
            return this;
        }

        public IVector3 Multiply(
            IVector3 vector3
        )
        {
            return new BabylonJSVector3(
                multiply(
                    new BabylonJSVector3(
                        vector3
                    )
                )
            );
        }

        public void AddInPlace(
            IVector3 vector3
        )
        {
            addInPlace(
                new BabylonJSVector3(
                    vector3
                )
            );
        }
    }
}
