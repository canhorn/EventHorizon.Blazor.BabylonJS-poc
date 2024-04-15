namespace EventHorizon.Game.Client.Engine.Systems.Camera.Register;

using System;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Systems.Camera.Model;
using MediatR;

public struct ManageCameraCommand : IRequest<StandardCommandResult>
{
    public string Name { get; }
    public ICamera Camera { get; }

    public ManageCameraCommand(string name, ICamera camera)
    {
        Name = name;
        Camera = camera;
    }
}
