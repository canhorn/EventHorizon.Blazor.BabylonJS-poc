namespace EventHorizon.Game.Editor.Zone.Services.Service;

using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using EventHorizon.Game.Editor.Services.Model.ClientEntity;
using EventHorizon.Game.Editor.Zone.Services.Api;

using Microsoft.AspNetCore.SignalR.Client;

public sealed class SignalrZoneAdminClientEntityApi : ZoneAdminClientEntityApi
{
    private readonly HubConnection? _hubConnection;

    internal SignalrZoneAdminClientEntityApi(HubConnection? hubConnection)
    {
        _hubConnection = hubConnection;
    }

    public Task<AdminClientEntityResponse> Create(IObjectEntityDetails entity)
    {
        if (_hubConnection.IsNotConnected())
        {
            return new AdminClientEntityResponse
            {
                Success = false,
                ErrorCode = ZoneAdminErrorCodes.NOT_CONNECTED,
            }.FromResult();
        }
        return _hubConnection.InvokeAsync<AdminClientEntityResponse>(
            "ClientEntity_Create",
            entity
        );
    }

    public Task<AdminClientEntityResponse> Delete(string clientEntityId)
    {
        if (_hubConnection.IsNotConnected())
        {
            return new AdminClientEntityResponse
            {
                Success = false,
                ErrorCode = ZoneAdminErrorCodes.NOT_CONNECTED,
            }.FromResult();
        }
        return _hubConnection.InvokeAsync<AdminClientEntityResponse>(
            "ClientEntity_Delete",
            clientEntityId
        );
    }

    public Task<AdminClientEntityResponse> Save(IObjectEntityDetails entity)
    {
        if (_hubConnection.IsNotConnected())
        {
            return new AdminClientEntityResponse
            {
                Success = false,
                ErrorCode = ZoneAdminErrorCodes.NOT_CONNECTED,
            }.FromResult();
        }
        return _hubConnection.InvokeAsync<AdminClientEntityResponse>(
            "ClientEntity_Save",
            entity
        );
    }
}
