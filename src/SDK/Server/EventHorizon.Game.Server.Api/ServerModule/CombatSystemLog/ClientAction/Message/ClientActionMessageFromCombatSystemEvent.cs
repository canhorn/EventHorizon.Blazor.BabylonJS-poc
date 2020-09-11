namespace EventHorizon.Game.Server.ServerModule.CombatSystemLog.ClientAction.Message
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes;
    using EventHorizon.Observer.Model;

    [ClientAction("MessageFromCombatSystem")]
    public struct ClientActionMessageFromCombatSystemEvent
        : IClientAction
    {
        public string Message { get; set; }
        [MaybeNull]
        public string MessageCode { get; set; }

        public ClientActionMessageFromCombatSystemEvent(
            IClientActionDataResolver resolver
        )
        {
            Message = resolver.Resolve<string>(
                "message"
            );
            MessageCode = resolver.Resolve<string>(
                "messageCode"
            );
        }

        public ClientActionMessageFromCombatSystemEvent(
            string message,
            string messageCode = ""
        )
        {
            Message = message;
            MessageCode = messageCode;
        }
    }

    public interface ClientActionMessageFromCombatSystemEventObserver
        : ArgumentObserver<ClientActionMessageFromCombatSystemEvent>
    {
    }
}
