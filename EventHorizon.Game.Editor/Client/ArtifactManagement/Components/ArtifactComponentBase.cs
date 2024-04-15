namespace EventHorizon.Game.Editor.Client.ArtifactManagement.Components;

using System.Threading.Tasks;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Server.Asset.Connection;
using EventHorizon.Game.Server.Asset.Finished;

public abstract class ArtifactComponentBase
    : ObservableComponentBase,
        ConnectedToAssetServerAdminObserver,
        AssetServerBackupFinishedEventObserver,
        AssetServerBackupUploadedEventObserver,
        AssetServerExportFinishedEventObserver,
        AssetServerExportUploadedEventObserver,
        AssetServerImportUploadedEventObserver
{
    protected abstract ArtifactType ArtifactType { get; }
    protected abstract Task HandleChange();

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await HandleChange();
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        await HandleChange();
    }

    public async Task Handle(ConnectedToAssetServerAdmin args)
    {
        await HandleChange();
        await InvokeAsync(StateHasChanged);
    }

    public async Task Handle(AssetServerBackupFinishedEvent args)
    {
        if (ArtifactType != (ArtifactType.Backup | ArtifactType.All))
        {
            return;
        }

        await HandleChange();
        await InvokeAsync(StateHasChanged);
    }

    public async Task Handle(AssetServerBackupUploadedEvent args)
    {
        if (ArtifactType != (ArtifactType.Backup | ArtifactType.All))
        {
            return;
        }

        await HandleChange();
        await InvokeAsync(StateHasChanged);
    }

    public async Task Handle(AssetServerExportFinishedEvent args)
    {
        if (ArtifactType != (ArtifactType.Export | ArtifactType.All))
        {
            return;
        }

        await HandleChange();
        await InvokeAsync(StateHasChanged);
    }

    public async Task Handle(AssetServerExportUploadedEvent args)
    {
        if (ArtifactType != (ArtifactType.Export | ArtifactType.All))
        {
            return;
        }

        await HandleChange();
        await InvokeAsync(StateHasChanged);
    }

    public async Task Handle(AssetServerImportUploadedEvent args)
    {
        if (ArtifactType != (ArtifactType.Import | ArtifactType.All))
        {
            return;
        }

        await HandleChange();
        await InvokeAsync(StateHasChanged);
    }
}
