namespace EventHorizon.Game.Client.Systems.Connection.Core.Api;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public interface CoreConnectionState
{
    bool IsConnected { get; }
    Task StartConnection(string serverUrl, string accessToken);
    Task StopConnection();
}
