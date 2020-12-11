namespace EventHorizon.Game.Editor.Zone.Services.Service
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Zone.Services.Api;
    using EventHorizon.Game.Editor.Zone.Services.Model;
    using Microsoft.AspNetCore.SignalR.Client;

    public sealed class SignalrZoneAdminApi
        : ZoneAdminApi
    {
        private readonly HubConnection _hubConnection;

        public ZoneAdminClientEntityApi ClientEntity { get; }
        public ZoneAdminCommandApi Command { get; }

        internal SignalrZoneAdminApi(
            HubConnection hubConnection
        )
        {
            _hubConnection = hubConnection;

            ClientEntity = new SignalrZoneAdminClientEntityApi(
                hubConnection
            );
            Command = new SignalrZoneAdminCommandApi(
                hubConnection
            );
        }

        public Task<ZoneInfo> GetZoneInfo()
        {
            if (_hubConnection == null)
            {
                return Task.FromResult<ZoneInfo>(
                    null
                );
            }
            return _hubConnection.InvokeAsync<ZoneInfo>(
                "ZoneInfo"
            );
        }
    }
}
