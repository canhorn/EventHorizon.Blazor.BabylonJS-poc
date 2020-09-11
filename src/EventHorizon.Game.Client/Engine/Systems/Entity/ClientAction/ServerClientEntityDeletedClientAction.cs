namespace EventHorizon.Game.Client.Engine.Systems.Entity.ClientAction
{
    using MediatR;

    // TODO: [ClientAction] : Finish Implementation
    //[ClientAction("SERVER_CLIENT_ENTITY_DELETED_CLIENT_ACTION_EVENT")]
    public struct ServerClientEntityDeletedClientAction : INotification
    {
        public static string ACTION_NAME = "SERVER_CLIENT_ENTITY_DELETED_CLIENT_ACTION_EVENT";
        // TODO: [ClientAction] : Change this to EntityId (that is the server id)
        public string GlobalId { get; set; }
    }
}
