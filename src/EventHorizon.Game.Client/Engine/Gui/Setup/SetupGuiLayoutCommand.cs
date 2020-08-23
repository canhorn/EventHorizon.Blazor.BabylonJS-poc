namespace EventHorizon.Game.Client.Engine.Gui.Setup
{
    using System;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Engine.Gui.Api;
    using MediatR;

    public struct SetupGuiLayoutCommand
        : IRequest<StandardCommandResult>
    {
        public string GuiId { get; }
        public IGuiLayoutData Layout { get; }
        public string? ParentControlId { get; }

        public SetupGuiLayoutCommand(
            string guiId, 
            IGuiLayoutData layout, 
            string? parentControlId
        )
        {
            GuiId = guiId;
            Layout = layout;
            ParentControlId = parentControlId;
        }
    }
}
