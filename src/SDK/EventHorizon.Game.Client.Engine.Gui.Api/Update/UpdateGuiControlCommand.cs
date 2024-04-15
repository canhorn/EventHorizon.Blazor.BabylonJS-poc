namespace EventHorizon.Game.Client.Engine.Gui.Update;

using System;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Gui.Api;
using MediatR;

public class UpdateGuiControlCommand : IRequest<StandardCommandResult>
{
    public string GuiId { get; }
    public IGuiControlData Control { get; }

    public UpdateGuiControlCommand(string guiId, IGuiControlData control)
    {
        GuiId = guiId;
        Control = control;
    }
}
