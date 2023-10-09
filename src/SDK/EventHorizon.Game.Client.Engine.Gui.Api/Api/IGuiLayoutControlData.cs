namespace EventHorizon.Game.Client.Engine.Gui.Api;

using System.Collections.Generic;

public interface IGuiLayoutControlData
{
    string Id { get; }
    int Sort { get; }
    int? Layer { get; }
    string TemplateId { get; }
    IGuiControlOptions? Options { get; }
    IGuiGridLocation? GridLocation { get; }
    IEnumerable<IGuiLayoutControlData>? ControlList { get; }
    object? LinkWith { get; }
}
