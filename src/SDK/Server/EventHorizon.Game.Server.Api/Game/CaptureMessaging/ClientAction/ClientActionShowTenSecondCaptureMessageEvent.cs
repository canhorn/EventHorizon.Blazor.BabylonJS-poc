namespace EventHorizon.Game.Server.Game.CaptureMessaging.ClientAction.Show
{
    using System;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes;
    using EventHorizon.Observer.Model;


    [ClientAction("Server.SHOW_TEN_SECOND_CAPTURE_MESSAGE")]
    public struct ClientActionShowTenSecondCaptureMessageEvent
        : IClientAction
    {
        public ClientActionShowTenSecondCaptureMessageEvent(
            IClientActionDataResolver _
        )
        {
        }

    }

    public interface ClientActionShowTenSecondCaptureMessageEventObserver
        : ArgumentObserver<ClientActionShowTenSecondCaptureMessageEvent>
    {
    }
}
