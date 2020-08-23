namespace EventHorizon.Game.Client.Systems.Connection.Core.Model
{
    using EventHorizon.Game.Client.Systems.Account.Api;

    public class ZoneDetailsModel
        : IZoneDetails
    {
        public string Id { get; set; } = string.Empty;
        public string ServerAddress { get; set; } = string.Empty;
    }
}