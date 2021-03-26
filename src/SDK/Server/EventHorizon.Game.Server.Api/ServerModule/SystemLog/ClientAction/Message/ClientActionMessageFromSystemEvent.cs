namespace EventHorizon.Game.Server.ServerModule.SystemLog.Message
{
    using EventHorizon.Game.Client.Engine.Gui.Api;
    using EventHorizon.Game.Client.Engine.Gui.Model;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes;
    using EventHorizon.Observer.Model;

    [ClientAction("MessageFromSystem")]
    public struct ClientActionMessageFromSystemEvent
        : IClientAction
    {
        public string Message { get; }
        public IGuiControlOptions? SenderControlOptions { get; }
        public IGuiControlOptions? MessageControlOptions { get; }

        public ClientActionMessageFromSystemEvent(
            IClientActionDataResolver resolver
        )
        {
            Message = resolver.Resolve<string>("message");
            SenderControlOptions = resolver.ResolveNullable<GuiControlOptionsModel>("senderControlOptions");
            MessageControlOptions = resolver.ResolveNullable<GuiControlOptionsModel>("messageControlOptions");
        }

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
