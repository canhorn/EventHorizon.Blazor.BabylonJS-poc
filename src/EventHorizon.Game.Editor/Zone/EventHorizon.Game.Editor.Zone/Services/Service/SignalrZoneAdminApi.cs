namespace EventHorizon.Game.Editor.Zone.Services.Service
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Zone.Services.Api;
    using EventHorizon.Game.Editor.Zone.Services.Model;
    using Microsoft.AspNetCore.SignalR.Client;

    public sealed class SignalrZoneAdminApi
        : ZoneAdminApi
    {
        private readonly HubConnection _hubConnection;

        internal SignalrZoneAdminApi(
            HubConnection hubConnection
        )
        {
            _hubConnection = hubConnection;
        }

        public Task<ZoneInfo> GetZoneInfo()
        {
            if (_hubConnection == null)
            {
                return new ZoneInfo()
                    .FromResult();
            }
            return _hubConnection.InvokeAsync<ZoneInfo>(
                "ZoneInfo"
            );
        }
    }
}
