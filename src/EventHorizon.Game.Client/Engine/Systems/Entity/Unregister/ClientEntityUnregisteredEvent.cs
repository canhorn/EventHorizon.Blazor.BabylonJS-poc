namespace EventHorizon.Game.Client.Engine.Systems.Entity.Unregister
{
    public class ClientEntityUnregisteredEvent
    {
        public string GlobalId { get; }

        public ClientEntityUnregisteredEvent(
            string globalId
        )
        {
            GlobalId = globalId;
        }
    }
}