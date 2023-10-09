namespace EventHorizon.Game.Client.Engine.Gui.Activate;

using System;

using EventHorizon.Game.Client.Core.Command.Model;

using MediatR;

public struct ActivateGuiCommand : IRequest<StandardCommandResult>
{
    public string GuiId { get; }

    public ActivateGuiCommand(string guiId)
    {
        GuiId = guiId;
    }
}
