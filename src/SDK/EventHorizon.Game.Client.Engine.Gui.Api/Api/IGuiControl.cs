namespace EventHorizon.Game.Client.Engine.Gui.Api;

using System;
using System.Diagnostics.CodeAnalysis;

using EventHorizon.Game.Client.Engine.Gui.Model;

public interface IGuiControl
{
    string Id { get; }
    GuiControlType Type { get; }
    bool IsVisible { get; set; }
    int Layer { get; set; } // TODO: Move into Options
    IGuiControlOptions Options { get; }

    [MaybeNull]
    string ParentId { get; }

    [MaybeNull]
    IGuiGridLocation GridLocation { get; }
    void Update(IGuiControlOptions options);
    void Dispose();
    void AddControl(IGuiControl guiControl);
    void LinkWith(object obj);
}
