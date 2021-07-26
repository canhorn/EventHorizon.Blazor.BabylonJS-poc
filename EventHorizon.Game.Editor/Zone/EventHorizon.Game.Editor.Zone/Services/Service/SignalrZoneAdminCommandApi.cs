namespace EventHorizon.Game.Editor.Zone.Services.Service
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Zone.Services.Api;
    using Microsoft.AspNetCore.SignalR.Client;

    public class SignalrZoneAdminCommandApi
        : ZoneAdminCommandApi
    {
        private readonly HubConnection? _hubConnection;

        internal SignalrZoneAdminCommandApi(
            HubConnection? hubConnection
        )
        {
            _hubConnection = hubConnection;
        }

        public async Task<StandardCommandResult> Send(
            string command,
            object data
        )
        {
            if (_hubConnection.IsNotConnected())
            {
                return new(
                    ZoneAdminErrorCodes.NOT_CONNECTED
                );
            }
            await _hubConnection.InvokeAsync(
                "Command",
                command,
                data
            );
            return new();
        }
    }
}
