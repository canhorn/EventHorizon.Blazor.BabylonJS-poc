namespace EventHorizon.Game.Client.Engine.Gui.Api;

using System;
using System.Diagnostics.CodeAnalysis;

public interface IGuiControlData
{
    string ControlId { get; }
    bool? IsVisible { get; }

    [MaybeNull]
    IGuiControlOptions Options { get; }

    [MaybeNull]
    object LinkWith { get; }
}
