namespace EventHorizon.Game.Editor.Zone.Services.ClientEntity.Delete;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Zone.Services.Api;
using MediatR;

public class DeleteClientEntityCommandHandler
    : IRequestHandler<DeleteClientEntityCommand, StandardCommandResult>
{
    private readonly ZoneAdminServices _zoneAdminServices;

    public DeleteClientEntityCommandHandler(ZoneAdminServices zoneAdminServices)
    {
        _zoneAdminServices = zoneAdminServices;
    }

    public async Task<StandardCommandResult> Handle(
        DeleteClientEntityCommand request,
        CancellationToken cancellationToken
    )
    {
        var result = await _zoneAdminServices.Api.ClientEntity.Delete(request.EntityId);
        if (result.Success.IsNotTrue())
        {
            return result.ErrorCode ?? ZoneAdminErrorCodes.BAD_API_REQUEST;
        }

        return new();
    }
}
