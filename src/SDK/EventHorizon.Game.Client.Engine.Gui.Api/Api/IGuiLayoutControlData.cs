namespace EventHorizon.Game.Client.Engine.Gui.Api
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public interface IGuiLayoutControlData
    {
        string Id { get; }
        int Sort { get; }
        int? Layer { get; }
        string TemplateId { get; }
        [MaybeNull]
        IGuiControlOptions Options { get; }
        [MaybeNull]
        IGuiGridLocation GridLocation { get; }
        [MaybeNull]
        IEnumerable<IGuiLayoutControlData> ControlList { get; }
        [MaybeNull]
        object LinkWith { get; }
    }
}