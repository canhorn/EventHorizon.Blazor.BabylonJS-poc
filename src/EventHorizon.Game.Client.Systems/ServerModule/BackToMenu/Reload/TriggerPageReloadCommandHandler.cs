namespace EventHorizon.Game.Client.Systems.ServerModule.BackToMenu.Reload;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Blazor.Interop;
using EventHorizon.Game.Client.Systems.Account.Query;
using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Stop;
using EventHorizon.Game.Server.ServerModule.BackToMenu.Reload;

using MediatR;

using Microsoft.Extensions.Logging;

public class TriggerPageReloadCommandHandler
    : IRequestHandler<TriggerPageReloadCommand>
{
    private readonly ILogger _logger;
    private readonly IMediator _mediator;

    public TriggerPageReloadCommandHandler(
        ILogger<TriggerPageReloadCommandHandler> logger,
        IMediator mediator
    )
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(
        TriggerPageReloadCommand request,
        CancellationToken cancellationToken
    )
    {
        // Disconnect from any Connections
        var result = await _mediator.Send(
            new QueryForZoneDetails(),
            cancellationToken
        );
        if (result.Success)
        {
            var serverAddress = result.Result.ServerAddress;
            _logger.LogDebug(
                "Stopping Player Connection: {DateTimeTriggered}",
                DateTime.UtcNow
            );
            await _mediator.Send(
                new StopPlayerZoneConnectionCommand(serverAddress),
                cancellationToken
            );
        }

        // Reload Page
        await EventHorizonBlazorInterop.RunScript(
            "reload_window",
            "window.location.reload();",
            new { }
        );

        return Unit.Value;
    }
}
