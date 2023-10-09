namespace EventHorizon.Game.Client.Systems.Connection.Zone.Player.Api;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public interface IPlayerZoneConnectionState
{
    bool IsConnected { get; }
    Task StartConnection(string serverUrl, string accessToken);
    Task StopConnection(CancellationToken cancellationToken);
    Task InvokeMethod(string methodName, IList<object> data);
    Task<T> InvokeMethodWithResult<T>(string methodName, IList<object> data)
        where T : class;
}
