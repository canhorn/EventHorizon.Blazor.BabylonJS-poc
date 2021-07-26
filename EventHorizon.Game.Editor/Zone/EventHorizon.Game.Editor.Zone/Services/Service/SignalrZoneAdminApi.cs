namespace EventHorizon.Game.Editor.Zone.Services.Service
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Zone.Services.Api;
    using EventHorizon.Game.Editor.Zone.Services.Model;
    using Microsoft.AspNetCore.SignalR.Client;

    public sealed class SignalrZoneAdminApi
        : ZoneAdminApi
    {
        private readonly HubConnection? _hubConnection;

        public ZoneAdminAgentApi Agent { get; }
        public ZoneAdminClientAssetsApi ClientAssets { get; }
        public ZoneAdminClientEntityApi ClientEntity { get; }
        public ZoneAdminCommandApi Command { get; }
        public ZoneAdminDataStorageApi DataStorage { get; }
        public ZoneAdminWizardApi Wizard { get; }

        internal SignalrZoneAdminApi(
            HubConnection? hubConnection
        )
        {
            _hubConnection = hubConnection;

            Agent = new SignalrZoneAdminAgentApi(
                hubConnection
            );
            ClientAssets = new SignalrZoneAdminClientAssetsApi(
                hubConnection
            );
            ClientEntity = new SignalrZoneAdminClientEntityApi(
                hubConnection
            );
            Command = new SignalrZoneAdminCommandApi(
                hubConnection
            );
            DataStorage = new SignalrZoneAdminDataStorageApi(
                hubConnection
            );
            Wizard = new SignalrZoneAdminWizardApi(
                hubConnection
            );
        }

        public Task<ZoneInfo?> GetZoneInfo()
        {
            if (_hubConnection.IsNotConnected())
            {
                return Task.FromResult<ZoneInfo?>(
                    null
                );
            }
            return _hubConnection.InvokeAsync<ZoneInfo?>(
                "ZoneInfo"
            );
        }
    }
}
