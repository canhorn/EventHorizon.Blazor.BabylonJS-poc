namespace EventHorizon.Game.Server.Asset.Api
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;

    public interface AssetServerAdminService
    {
        AssetServerExportAdminApi ExportApi { get; }

        AssetServerFileManagementAdminApi FileManagementApi {get;}

        Task<StandardCommandResult> Connect(
            string accessToken,
            CancellationToken cancellationToken
        );
    }
}
