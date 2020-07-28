namespace EventHorizon.Game.Client.Systems.Connection.Zone.Player.Api
{
    using System.Threading.Tasks;

    public interface IZonePlayerConnectionState
    {
        bool IsConnected { get; }
        Task StartConnection(
            string serverUrl,
            string accessToken
        );
        Task StopConnection();
    }
}
