namespace EventHorizon.Game.Client.Engine.Systems.Camera.Unregister;

using System;

using EventHorizon.Game.Client.Core.Command.Model;

using MediatR;

public class DisposeOfCameraCommand : IRequest<StandardCommandResult>
{
    public string Name { get; }

    public DisposeOfCameraCommand(string name)
    {
        Name = name;
    }
}
