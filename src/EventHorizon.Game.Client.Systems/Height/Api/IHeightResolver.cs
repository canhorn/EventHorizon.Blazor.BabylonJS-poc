namespace EventHorizon.Game.Client.Systems.Height.Api
{
    public interface IHeightResolver
    {
        decimal FindHeight(
            decimal x, 
            decimal z
        );
    }
}
