namespace EventHorizon.Game.Client.Engine.Gui.Model.Options;

using System;
using System.Diagnostics.CodeAnalysis;

// TODO: Flatten this into BabylonJSGuiBar IGuiControlOptions
public class GuiBarPercentageBarOptionModel
{
    [MaybeNull]
    public string Background { get; set; }

    [MaybeNull]
    public string BarDirection { get; set; }
}
