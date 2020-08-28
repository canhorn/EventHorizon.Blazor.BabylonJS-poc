namespace EventHorizon.Game.Client.Systems.Connection.Zone.Player.Model
{
    using EventHorizon.Game.Client.Engine.Systems.Entity.Model;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Api;

    public class PlayerZoneDetailsModel
        : ObjectEntityDetailsModel, IPlayerZoneDetails
    {
        public string PlayerId { get; set; } = string.Empty;
    }
}