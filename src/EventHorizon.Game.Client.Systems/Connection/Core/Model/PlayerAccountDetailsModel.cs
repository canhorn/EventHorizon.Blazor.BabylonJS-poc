namespace EventHorizon.Game.Client.Systems.Connection.Core.Model;

using EventHorizon.Game.Client.Systems.Account.Api;

public class PlayerAccountDetailsModel : IPlayerAccountDetails
{
    public string Username { get; set; } = string.Empty;
    public string Locale { get; set; } = string.Empty;
}
