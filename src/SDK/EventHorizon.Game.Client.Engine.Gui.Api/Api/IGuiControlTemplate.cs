namespace EventHorizon.Game.Client.Engine.Gui.Api;

using System;
using System.Diagnostics.CodeAnalysis;
using EventHorizon.Game.Client.Engine.Gui.Model;

public interface IGuiControlTemplate
{
    string Id { get; }
    GuiControlType Type { get; }

    [MaybeNull]
    IGuiGridLocation GridLocation { get; }
    IGuiControlOptions Options { get; }
}
