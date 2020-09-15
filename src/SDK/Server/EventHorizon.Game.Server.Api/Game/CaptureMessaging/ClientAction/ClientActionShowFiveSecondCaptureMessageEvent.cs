namespace EventHorizon.Game.Server.Game.CaptureMessaging.ClientAction.Show
{
    using System;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes;
    using EventHorizon.Observer.Model;

    [ClientAction("Server.SHOW_FIVE_SECOND_CAPTURE_MESSAGE")]
    public struct ClientActionShowFiveSecondCaptureMessageEvent
        : IClientAction
    {
        public ClientActionShowFiveSecondCaptureMessageEvent(
            IClientActionDataResolver _
        )
        {
        }
    }

    public interface ClientActionShowFiveSecondCaptureMessageEventObserver
        : ArgumentObserver<ClientActionShowFiveSecondCaptureMessageEvent>
    {
    }
}
