namespace EventHorizon.Game.Client.Systems.Account.Api
{
    using System;

    public interface IAccountInfo
    {
        IPlayerAccountDetails Player { get; }
        IZoneDetails Zone { get; }
    }
}
