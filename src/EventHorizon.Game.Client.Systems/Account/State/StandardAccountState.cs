﻿namespace EventHorizon.Game.Client.Systems.Account.State;

using EventHorizon.Game.Client.Systems.Account.Api;
using EventHorizon.Game.Client.Systems.Connection.Core.Model;

public class StandardAccountState : IAccountState
{
    public string? AccessToken { get; private set; }
    public string? AccountLoginUrl { get; private set; }
    public IAccountInfo? User { get; private set; }

    public void Reset()
    {
        AccessToken = null;
        AccountLoginUrl = null;
        User = null;
    }

    public void SetAccountUser(IAccountInfo user)
    {
        User = user;
    }

    public void Setup(string accessToken, string accountLoginUrl)
    {
        AccessToken = accessToken;
        AccountLoginUrl = accountLoginUrl;
    }
}
