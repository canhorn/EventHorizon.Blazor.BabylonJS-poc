namespace EventHorizon.Game.Client.Engine.Gui.Model
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using EventHorizon.Game.Client.Engine.Gui.Api;

    public class GuiControlTemplateModel
        : IGuiControlTemplate
    {
        public string Id { get; set; }
        public GuiControlType Type { get; }
        [MaybeNull]
        public IGuiGridLocation GridLocation { get; }
        public IGuiControlOptions Options { get; }

        public GuiControlTemplateModel(
            string id,
            GuiControlType type,
            IGuiControlOptions options,
            [MaybeNull] IGuiGridLocation gridLocation = null
        )
        {
            Id = id;
            Type = type;
            Options = options;
            GridLocation = gridLocation;
        }
    }
}
