namespace EventHorizon.Game.Client.Engine.Systems.Player.Api
{
    using System;

    public interface IPlayerDetails
    {
        string PlayerId { get; }
        string AccessToken { get; }
    }
}
