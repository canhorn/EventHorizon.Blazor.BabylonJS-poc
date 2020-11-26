namespace EventHorizon.Game.Editor.Zone.Services.Api
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Editor.Services.Model.ClientEntity;

    public interface ZoneAdminClientEntityApi
    {
        Task<AdminClientEntityResponse> Save(
            IObjectEntityDetails entity
        );
        Task<AdminClientEntityResponse> Create(
            IObjectEntityDetails entity
        );
        Task<AdminClientEntityResponse> Delete(
            string clientEntityId
        );
    }
}
