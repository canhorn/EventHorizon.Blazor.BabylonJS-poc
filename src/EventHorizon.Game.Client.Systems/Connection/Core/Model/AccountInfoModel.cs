namespace EventHorizon.Game.Client.Systems.Connection.Core.Model;

using EventHorizon.Game.Client.Systems.Account.Api;

public class AccountInfoModel : IAccountInfo
{
    public PlayerAccountDetailsModel Player { get; set; } =
        new PlayerAccountDetailsModel();
    IPlayerAccountDetails IAccountInfo.Player => Player;
    public ZoneDetailsModel Zone { get; set; } = new ZoneDetailsModel();
    IZoneDetails IAccountInfo.Zone => Zone;
}
