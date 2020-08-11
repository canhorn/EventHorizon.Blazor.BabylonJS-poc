namespace EventHorizon.Game.Client.Engine.Entity.Api
{
    public interface IVector3
    {
        decimal X { get; }
        decimal Y { get; }
        decimal Z { get; }

        void Set(
            decimal x,
            decimal y,
            decimal z
        );
        void Set(
            IVector3 vector3
        );
        IVector3 Clone();
        IVector3 Subtract(
            IVector3 currentPosition
        );
        decimal Length();
        IVector3 Normalize();
        IVector3 Multiply(
            IVector3 standardVector3
        );
        void AddInPlace(
            IVector3 velocity
        );
    }
}
