namespace EventHorizon.Game.Editor.Zone.Services.Service
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Editor.Zone.Services.Api;
    using EventHorizon.Game.Editor.Zone.Services.Model.Agent;
    using Microsoft.AspNetCore.SignalR.Client;

    public sealed class SignalrZoneAdminAgentApi
        : ZoneAdminAgentApi
    {
        private readonly HubConnection _hubConnection;

        internal SignalrZoneAdminAgentApi(
            HubConnection hubConnection
        )
        {
            _hubConnection = hubConnection;

        }

        public Task<AdminAgentEntityResponse> CreateEntity(
            IObjectEntityDetails entity
        )
        {
            if (_hubConnection == null)
            {
                return new AdminAgentEntityResponse
                {
                    Success = false,
                    ErrorCode = ZoneAdminErrorCodes.NOT_CONNECTED,
                }.FromResult();
            }
            return _hubConnection.InvokeAsync<AdminAgentEntityResponse>(
                "Agent_EntityCreate",
                entity
            );
        }

        public Task<AdminAgentEntityResponse> DeleteEntity(
            string agentEntityId
        )
        {
            if (_hubConnection == null)
            {
                return new AdminAgentEntityResponse
                {
                    Success = false,
                    ErrorCode = ZoneAdminErrorCodes.NOT_CONNECTED,
                }.FromResult();
            }
            return _hubConnection.InvokeAsync<AdminAgentEntityResponse>(
                "Agent_EntityDelete",
                agentEntityId
            );
        }

        public Task<AdminAgentEntityResponse> SaveEntity(
            IObjectEntityDetails entity
        )
        {
            if (_hubConnection == null)
            {
                return new AdminAgentEntityResponse
                {
                    Success = false,
                    ErrorCode = ZoneAdminErrorCodes.NOT_CONNECTED,
                }.FromResult();
            }
            return _hubConnection.InvokeAsync<AdminAgentEntityResponse>(
                "Agent_EntitySave",
                entity
            );
        }
    }
}
