namespace EventHorizon.Game.Client.Engine.Gui.Register
{
    using EventHorizon.Game.Client.Core.Command.Api;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Engine.Gui.Api;
    using MediatR;

    public struct RegisterGuiLayoutDataCommand
        : IRequest<StandardCommandResult>
    {
        public IGuiLayoutData LayoutData { get; }

        public RegisterGuiLayoutDataCommand(
            IGuiLayoutData layoutData
        )
        {
            LayoutData = layoutData;
        }
    }
}
