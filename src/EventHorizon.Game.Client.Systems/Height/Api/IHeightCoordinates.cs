namespace EventHorizon.Game.Client.Systems.Height.Api
{
    public interface IHeightCoordinates
    {
        decimal getHeightAtCoordinates(
            decimal x,
            decimal z
        );
    }
}
