namespace EventHorizon.Game.Client.Systems.Account.Api
{
    public interface IPlayerAccountDetails
    {
        string Username { get; }
        string Locale { get; }
    }
}