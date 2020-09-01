namespace EventHorizon.Game.Client.Engine.Gui.Api
{
    using System;

    public interface IGuiControlData
    {
        string ControlId { get; }
        bool? IsVisible { get; }
        IGuiControlOptions? Options { get; }
        object? LinkWith { get; }
    }
}
