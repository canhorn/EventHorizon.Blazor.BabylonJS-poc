namespace EventHorizon.Game.Client.Engine.Systems.Camera.Register;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Systems.Camera.Api;
using MediatR;

public class ManageCameraCommandHandler
    : IRequestHandler<ManageCameraCommand, StandardCommandResult>
{
    private readonly ICameraState _state;

    public ManageCameraCommandHandler(ICameraState state)
    {
        _state = state;
    }

    public Task<StandardCommandResult> Handle(
        ManageCameraCommand request,
        CancellationToken cancellationToken
    )
    {
        _state.Manage(request.Name, request.Camera);
        return new StandardCommandResult().FromResult();
    }
}
