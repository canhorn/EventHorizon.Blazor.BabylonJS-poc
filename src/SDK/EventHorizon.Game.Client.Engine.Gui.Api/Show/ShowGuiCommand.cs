namespace EventHorizon.Game.Client.Engine.Gui.Show;

using System;

using EventHorizon.Game.Client.Core.Command.Model;

using MediatR;

public struct ShowGuiCommand : IRequest<StandardCommandResult>
{
    public string Id { get; }

    public ShowGuiCommand(string id)
    {
        Id = id;
    }
}
