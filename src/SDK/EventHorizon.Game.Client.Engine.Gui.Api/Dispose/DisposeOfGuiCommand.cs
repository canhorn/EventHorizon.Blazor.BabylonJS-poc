namespace EventHorizon.Game.Client.Engine.Gui.Dispose;

using System;
using EventHorizon.Game.Client.Core.Command.Model;
using MediatR;

public class DisposeOfGuiCommand : IRequest<StandardCommandResult>
{
    public string GuiId { get; }

    public DisposeOfGuiCommand(string guiId)
    {
        GuiId = guiId;
    }
}
