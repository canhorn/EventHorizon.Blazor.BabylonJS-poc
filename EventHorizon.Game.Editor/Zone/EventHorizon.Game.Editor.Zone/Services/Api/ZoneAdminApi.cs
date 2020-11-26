namespace EventHorizon.Game.Editor.Zone.Services.Api
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Zone.Services.Model;

    public interface ZoneAdminApi
    {
        Task<ZoneInfo> GetZoneInfo();
        ZoneAdminClientEntityApi ClientEntity { get; }
    }
}
