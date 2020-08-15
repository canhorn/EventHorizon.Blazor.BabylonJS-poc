namespace EventHorizon.Game.Client.Engine.Systems.Camera.Set
{
    using System;
    using EventHorizon.Game.Client.Core.Command.Model;
    using MediatR;

    public class SetActiveCameraCommand
        : IRequest<StandardCommandResult>
    {
        public string Name { get; }

        public SetActiveCameraCommand(
            string name
        )
        {
            Name = name;
        }
    }
}
