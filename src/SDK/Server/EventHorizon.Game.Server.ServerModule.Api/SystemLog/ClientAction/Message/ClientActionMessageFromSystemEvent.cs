namespace EventHorizon.Game.Server.ServerModule.SystemLog.Message
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Gui.Api;
    using EventHorizon.Game.Client.Engine.Gui.Model;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    // TODO: [ClientAction] : Finish Implementation
    //[ClientAction("MessageFromSystem")]
    public struct ClientActionMessageFromSystemEvent : INotification, IClientAction
    {
        public string Message { get; }
        public IGuiControlOptions SenderControlOptions { get; }
        public IGuiControlOptions MessageControlOptions { get; }

        public ClientActionMessageFromSystemEvent(IClientActionDataResolver _)
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
        : ArgumentObserver<ClientActionMessageFromSystemEvent> { }

    // TODO: Move this into an Implementation Project, Remove from the SDK
    public class ClientActionMessageFromSystemEventHandler
        : INotificationHandler<ClientActionMessageFromSystemEvent>
    {
        private readonly ObserverState _observer;

        public ClientActionMessageFromSystemEventHandler(ObserverState observer)
        {
            _observer = observer;
        }

        public Task Handle(
            ClientActionMessageFromSystemEvent notification,
            CancellationToken cancellationToken
        ) =>
            _observer.Trigger<
                ClientActionMessageFromSystemEventObserver,
                ClientActionMessageFromSystemEvent
            >(notification, cancellationToken);
    }
}
