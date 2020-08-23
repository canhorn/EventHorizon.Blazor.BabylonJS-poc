namespace EventHorizon.Game.Client.Engine.Gui.Model
{
    using System;
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Engine.Gui.Api;

    public class GuiLayoutDataModel
        : IGuiLayoutData
    {
        public string Id { get; set; } = string.Empty;
        public int Sort { get; set; }
        public List<GuiLayoutControlDataModel> ControlList { get; set; } = new List<GuiLayoutControlDataModel>();
        IEnumerable<IGuiLayoutControlData> IGuiLayoutData.ControlList => ControlList;
        public string? InitializeScript { get; set; }
        public string? ActivateScript { get; set; }
        public string? DisposeScript { get; set; }
        public string? UpdateScript { get; set; }
        public string? DrawScript { get; set; }
    }
}
