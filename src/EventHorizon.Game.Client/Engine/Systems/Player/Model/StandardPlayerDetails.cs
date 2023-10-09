namespace EventHorizon.Game.Client.Engine.Systems.Player.Model;

using System;

using EventHorizon.Game.Client.Engine.Systems.Player.Api;

public class StandardPlayerDetails : IPlayerDetails
{
    public string PlayerId { get; }
    public string AccessToken { get; }

    public StandardPlayerDetails(string playerId, string accessToken)
    {
        PlayerId = playerId;
        AccessToken = accessToken;
    }
}
