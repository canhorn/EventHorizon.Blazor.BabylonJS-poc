namespace EventHorizon.Game.Client.Engine.Gui.Create
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Engine.Gui.Api;
    using MediatR;

    public struct CreateGuiCommand
        : IRequest<StandardCommandResult>
    {
        public string GuiId { get; }
        public string LayoutId { get; }
        [MaybeNull]
        public IEnumerable<IGuiControlData> ControlDataList { get; }
        [MaybeNull]
        public string ParentControlId { get; }

        public CreateGuiCommand(
            string guiId,
            string layoutId,
            [MaybeNull] IEnumerable<IGuiControlData> controlDataList = null,
            [MaybeNull] string parentControlId = null
        )
        {
            GuiId = guiId;
            LayoutId = layoutId;
            ControlDataList = controlDataList;
            ParentControlId = parentControlId;
        }
    }
}
