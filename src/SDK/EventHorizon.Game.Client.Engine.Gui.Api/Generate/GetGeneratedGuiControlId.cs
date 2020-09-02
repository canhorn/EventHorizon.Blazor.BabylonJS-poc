namespace EventHorizon.Game.Client.Engine.Gui.Generate
{
    using System;
    using MediatR;

    public struct GetGeneratedGuiControlId
        : IRequest<string>
    {
        public string GuiId { get; }
        public string ControlId { get; }

        public GetGeneratedGuiControlId(
            string guiId,
            string controlId
        )
        {
            GuiId = guiId;
            ControlId = controlId;
        }
    }
}
