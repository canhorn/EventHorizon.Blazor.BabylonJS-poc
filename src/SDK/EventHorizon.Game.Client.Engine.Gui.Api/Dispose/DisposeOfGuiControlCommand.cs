namespace EventHorizon.Game.Client.Engine.Gui.Dispose;

using System;

using EventHorizon.Game.Client.Core.Command.Model;

using MediatR;

public struct DisposeOfGuiControlCommand : IRequest<StandardCommandResult>
{
    public string GuiId { get; }
    public string ControlId { get; }

    public DisposeOfGuiControlCommand(string guiId, string controlId)
    {
        GuiId = guiId;
        ControlId = controlId;
    }
}
