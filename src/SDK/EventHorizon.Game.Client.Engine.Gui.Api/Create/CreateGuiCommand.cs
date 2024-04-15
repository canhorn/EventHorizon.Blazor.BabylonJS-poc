namespace EventHorizon.Game.Client.Engine.Gui.Create;

using System.Collections.Generic;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Gui.Api;
using MediatR;

public struct CreateGuiCommand : IRequest<StandardCommandResult>
{
    public string GuiId { get; }
    public string LayoutId { get; }
    public IEnumerable<IGuiControlData>? ControlDataList { get; }
    public string? ParentControlId { get; }

    public CreateGuiCommand(
        string guiId,
        string layoutId,
        IEnumerable<IGuiControlData>? controlDataList = null,
        string? parentControlId = null
    )
    {
        GuiId = guiId;
        LayoutId = layoutId;
        ControlDataList = controlDataList;
        ParentControlId = parentControlId;
    }
}
