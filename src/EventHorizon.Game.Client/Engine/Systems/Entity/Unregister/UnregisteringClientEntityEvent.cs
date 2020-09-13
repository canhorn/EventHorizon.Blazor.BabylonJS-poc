namespace EventHorizon.Game.Client.Engine.Systems.Entity.Unregister
{
    using MediatR;

    public struct UnregisteringClientEntityEvent
        : INotification
    {
        public string GlobalId { get; }

        public UnregisteringClientEntityEvent(
            string globalId
        )
        {
            GlobalId = globalId;
        }
    }
}