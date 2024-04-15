namespace EventHorizon.Game.Client.Engine.Systems.Camera.Set;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Core.Exceptions;
using EventHorizon.Game.Client.Engine.Systems.Camera.Api;
using MediatR;
using Microsoft.Extensions.Logging;

public class SetActiveCameraCommandHandler
    : IRequestHandler<SetActiveCameraCommand, StandardCommandResult>
{
    private readonly ILogger _logger;
    private readonly ICameraState _state;

    public SetActiveCameraCommandHandler(
        ILogger<SetActiveCameraCommandHandler> logger,
        ICameraState state
    )
    {
        _logger = logger;
        _state = state;
    }

    public Task<StandardCommandResult> Handle(
        SetActiveCameraCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            _state.SetActive(request.Name);

            return new StandardCommandResult().FromResult();
        }
        catch (GameException ex)
        {
            _logger.LogError(ex, "Failed to set Active Camera.");
            return new StandardCommandResult(ex.ErrorCode).FromResult();
        }
    }
}
