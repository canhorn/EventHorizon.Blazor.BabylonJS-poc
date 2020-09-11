namespace EventHorizon.Game.Client.Engine.Gui.Setup
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Engine.Gui.Api;
    using MediatR;

    public struct SetupGuiLayoutCommand
        : IRequest<StandardCommandResult>
    {
        public string GuiId { get; }
        public IGuiLayoutData Layout { get; }
        [MaybeNull]
        public string ParentControlId { get; }

        public SetupGuiLayoutCommand(
            string guiId,
            IGuiLayoutData layout,
            [MaybeNull] string parentControlId
        )
        {
            GuiId = guiId;
            Layout = layout;
            ParentControlId = parentControlId;
        }
    }
}
