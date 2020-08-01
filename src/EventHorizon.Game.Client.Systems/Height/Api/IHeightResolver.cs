namespace EventHorizon.Game.Client.Systems.Height.Api
{
    public interface IHeightResolver
    {
        decimal findHeight(
            decimal x, 
            decimal z
        );
    }
}
