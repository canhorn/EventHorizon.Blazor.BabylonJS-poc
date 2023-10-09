namespace EventHorizon.Game.Client.Engine.Gui.Model;

using System;
using System.Collections.Generic;

using EventHorizon.Game.Client.Engine.Gui.Api;

public class GuiLayoutDataModel : IGuiLayoutData
{
    public string Id { get; set; } = string.Empty;
    public int Sort { get; set; } = 0;
    public List<GuiLayoutControlDataModel> ControlList { get; set; } =
        new List<GuiLayoutControlDataModel>();
    IEnumerable<IGuiLayoutControlData> IGuiLayoutData.ControlList =>
        ControlList;
    public string InitializeScript { get; set; } = string.Empty;
    public string ActivateScript { get; set; } = string.Empty;
    public string DisposeScript { get; set; } = string.Empty;
    public string UpdateScript { get; set; } = string.Empty;
    public string DrawScript { get; set; } = string.Empty;
}
