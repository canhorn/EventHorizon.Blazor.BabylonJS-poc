namespace EventHorizon.Game.Server.ServerModule.CombatSystemLog.ClientAction.Message
{
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes;
    using EventHorizon.Observer.Model;

    [ClientAction("MessageFromCombatSystem")]
    public struct ClientActionMessageFromCombatSystemEvent
        : IClientAction
    {
        public string Message { get; set; }
        public string? MessageCode { get; set; }

        public ClientActionMessageFromCombatSystemEvent(
            IClientActionDataResolver resolver
        )
        {
            Message = resolver.Resolve<string>(
                "message"
            );
            MessageCode = resolver.ResolveNullable<string>(
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
