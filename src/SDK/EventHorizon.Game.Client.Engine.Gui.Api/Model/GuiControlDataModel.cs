namespace EventHorizon.Game.Client.Engine.Gui.Model
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using EventHorizon.Game.Client.Engine.Gui.Api;

    public class GuiControlDataModel
        : IGuiControlData
    {
        public string ControlId { get; set; } = string.Empty;
        public bool? IsVisible { get; set; }
        [MaybeNull]
        public GuiControlOptionsModel Options { get; set; }
        [MaybeNull]
        IGuiControlOptions IGuiControlData.Options => Options;
        [MaybeNull]
        public object LinkWith { get; set; }
    }
}
