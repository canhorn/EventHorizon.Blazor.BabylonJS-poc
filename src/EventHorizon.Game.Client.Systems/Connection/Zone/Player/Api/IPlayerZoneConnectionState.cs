namespace EventHorizon.Game.Client.Systems.Connection.Zone.Player.Api
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPlayerZoneConnectionState
    {
        bool IsConnected { get; }
        Task StartConnection(
            string serverUrl,
            string accessToken
        );
        Task StopConnection();
        Task InvokeMethod(
            string methodName,
            IList<object> data
        );
    }
}
