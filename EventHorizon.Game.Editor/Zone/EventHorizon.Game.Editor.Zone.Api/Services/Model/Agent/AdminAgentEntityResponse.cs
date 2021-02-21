namespace EventHorizon.Game.Editor.Zone.Services.Model.Agent
{
    using System;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Model;

    public class AdminAgentEntityResponse
    {
        public bool Success { get; set; }
        public string ErrorCode { get; set; }
        public ObjectEntityDetailsModel ClientEntity { get; set; }
    }
}
