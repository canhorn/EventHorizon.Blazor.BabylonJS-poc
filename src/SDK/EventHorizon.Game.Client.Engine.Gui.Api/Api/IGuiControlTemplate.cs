namespace EventHorizon.Game.Client.Engine.Gui.Api
{
    using System;
    using EventHorizon.Game.Client.Engine.Gui.Model;

    public interface IGuiControlTemplate
    {
        string Id { get; }
        GuiControlType Type { get; }
        IGuiGridLocation? GridLocation { get; }
        IGuiControlOptions Options { get; }
    }
}
