namespace EventHorizon.Game.Server.Asset.Api;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;

public interface AssetServerAdminService
{
    AssetServerCommonAdminApi CommonApi { get; }

    AssetServerBackupAdminApi BackupApi { get; }

    AssetServerExportAdminApi ExportApi { get; }

    AssetServerFileManagementAdminApi FileManagementApi { get; }

    Task<StandardCommandResult> Connect(
        string accessToken,
        CancellationToken cancellationToken
    );
}
