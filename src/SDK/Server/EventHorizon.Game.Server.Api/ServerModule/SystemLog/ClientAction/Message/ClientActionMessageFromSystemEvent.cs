namespace EventHorizon.Game.Server.ServerModule.SystemLog.Message
{
    using EventHorizon.Game.Client.Engine.Gui.Api;
    using EventHorizon.Game.Client.Engine.Gui.Model;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    // TODO: [ClientAction] : Finish Implementation
    //[ClientAction("MessageFromSystem")]
    public struct ClientActionMessageFromSystemEvent
        : IClientAction
    {
        public string Message { get; }
        public IGuiControlOptions SenderControlOptions { get; }
        public IGuiControlOptions MessageControlOptions { get; }

        public ClientActionMessageFromSystemEvent(
            IClientActionDataResolver _
        )
        {
            Message = _.Resolve<string>("message");
            SenderControlOptions = _.Resolve<GuiControlOptionsModel>("senderControlOptions");
            MessageControlOptions = _.Resolve<GuiControlOptionsModel>("messageControlOptions");
        }

        // TODO: Check to make sure this can be triggered
        public ClientActionMessageFromSystemEvent(
            string message,
            IGuiControlOptions senderControlOptions,
            IGuiControlOptions messageControlOptions
        )
        {
            Message = message;
            SenderControlOptions = senderControlOptions;
            MessageControlOptions = messageControlOptions;
        }
    }

    public interface ClientActionMessageFromSystemEventObserver
        : ArgumentObserver<ClientActionMessageFromSystemEvent>
    {
    }
}
