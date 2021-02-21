namespace EventHorizon.Game.Editor.Zone.Services.Api
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Editor.Zone.Services.Model.Agent;

    public interface ZoneAdminAgentApi
    {
        Task<AdminAgentEntityResponse> SaveEntity(
            IObjectEntityDetails entity
        );
        Task<AdminAgentEntityResponse> CreateEntity(
            IObjectEntityDetails entity
        );
        Task<AdminAgentEntityResponse> DeleteEntity(
            string clientEntityId
        );
    }
}
