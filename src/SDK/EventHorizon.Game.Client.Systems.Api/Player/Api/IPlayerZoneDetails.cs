namespace EventHorizon.Game.Client.Systems.Player.Api
{
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;

    public interface IPlayerZoneDetails
        : IObjectEntityDetails
    {
        string PlayerId { get; }
    }
}
