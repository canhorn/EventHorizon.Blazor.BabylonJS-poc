namespace EventHorizon.Game.Client.Engine.Gui.Model;

using System;
using System.Diagnostics.CodeAnalysis;
using EventHorizon.Game.Client.Engine.Gui.Api;

public class GuiControlDataModel : IGuiControlData
{
    public string ControlId { get; set; } = string.Empty;
    public bool? IsVisible { get; set; }
    public GuiControlOptionsModel? Options { get; set; }
    IGuiControlOptions? IGuiControlData.Options => Options;
    public object? LinkWith { get; set; }
}
