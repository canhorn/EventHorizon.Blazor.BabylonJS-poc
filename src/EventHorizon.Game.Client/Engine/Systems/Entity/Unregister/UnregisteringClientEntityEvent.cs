namespace EventHorizon.Game.Client.Engine.Systems.Entity.Unregister
{
    public class UnregisteringClientEntityEvent
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