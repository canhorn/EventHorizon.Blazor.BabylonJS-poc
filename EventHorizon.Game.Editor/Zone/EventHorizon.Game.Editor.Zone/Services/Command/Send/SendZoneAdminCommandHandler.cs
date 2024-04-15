namespace EventHorizon.Game.Editor.Client.Zone.Services.Command.Send;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Zone.Services.Api;
using MediatR;

public class SendZoneAdminCommandHandler
    : IRequestHandler<SendZoneAdminCommand, StandardCommandResult>
{
    private readonly ZoneAdminServices _zoneAdminServices;

    public SendZoneAdminCommandHandler(ZoneAdminServices zoneAdminServices)
    {
        _zoneAdminServices = zoneAdminServices;
    }

    public async Task<StandardCommandResult> Handle(
        SendZoneAdminCommand request,
        CancellationToken cancellationToken
    )
    {
        var result = await _zoneAdminServices.Api.Command.Send(request.Command, request.Data);
        if (result.Success.IsNotTrue())
        {
            return new(result.ErrorCode);
        }

        return new();
    }
}
