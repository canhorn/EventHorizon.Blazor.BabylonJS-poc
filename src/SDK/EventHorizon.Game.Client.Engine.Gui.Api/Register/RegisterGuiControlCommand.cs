namespace EventHorizon.Game.Client.Engine.Gui.Register;

using System;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Gui.Api;
using MediatR;

public struct RegisterGuiControlCommand : IRequest<StandardCommandResult>
{
    public string GuiId { get; }
    public IGuiLayoutControlData Control { get; }

    public RegisterGuiControlCommand(string guiId, IGuiLayoutControlData control)
    {
        GuiId = guiId;
        Control = control;
    }
}
