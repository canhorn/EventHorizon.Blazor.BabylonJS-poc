namespace EventHorizon.Game.Client.Systems.Height.Set
{
    using EventHorizon.Game.Client.Systems.Height.Api;
    using MediatR;

    public class SetHeightResolverCoordinatesCommand
        : IRequest
    {
        public IHeightCoordinates HeightCoordinates { get; }

        public SetHeightResolverCoordinatesCommand(
            IHeightCoordinates heightCoordinates
        )
        {
            HeightCoordinates = heightCoordinates;
        }

    }
}
